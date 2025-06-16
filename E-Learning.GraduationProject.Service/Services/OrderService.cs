using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Orders;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Entities.Orders;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Orders;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OrderService(
            IBasketService basketService,
           IUnitOfWork unitOfWork,
           IPaymentService paymentService,
           IMapper mapper
            )
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<OrderToReturnDto?> CreateOrderAsync(string buyerEmail, string basketId)
        {
            var basket = await _basketService.GetBasketByIdAsync(basketId);
            if (basket is null) return null;

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count > 0)
            {
                // mapping from basket to generate order
                foreach (var item in basket.Items)
                {
                    var course = await _unitOfWork.Repository<Course, int>().GetByIdAsync(item.CourseId);

                    var courseOrder = new CourseOrder(course.Id, course.Title, course.ThumbnailUrl);

                    var orderItem = new OrderItem(courseOrder, course.Price);

                    orderItems.Add(orderItem);
                }
            }

            // Delete existing order if same PaymentIntentId exists
            if (!basket.PaymentIntentId.IsNullOrEmpty())
            {
                var spec = new OrderPaymentIntentSpecifications(basket.PaymentIntentId);

                var existOrder = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);

                _unitOfWork.Repository<Order, int>().Delete(existOrder);

                var res = await _unitOfWork.CompleteAsync();

                if (res <= 0) return null;

            }

            var paymentResult = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            var order = new Order()
            {
                BuyerMail = buyerEmail,
                OrderItems = orderItems,
                PaymentIntentId = paymentResult.PaymentIntentId,
                TotalAmount = orderItems.Sum(I => I.Price)
            };

            await _unitOfWork.Repository<Order, int>().AddAsync(order);
            var ret = await _unitOfWork.CompleteAsync();

           return ret > 0  ? _mapper.Map<OrderToReturnDto>(order): null;


        }

        public async Task<IEnumerable<OrderToReturnDto>?> GetAllOrdersForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order, int>().GetAllWithSpecAsync(spec);

            return orders is null ? null : _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications(buyerEmail,orderId);
            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);

            return order is null ? null : _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
