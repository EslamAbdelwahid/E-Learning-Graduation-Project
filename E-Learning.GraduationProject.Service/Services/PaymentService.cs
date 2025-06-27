using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Dtos.Orders;
using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Entities.Orders;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Orders;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;

        public PaymentService(
            IBasketService basketService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            IStudentService studentService
            )
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _studentService = studentService;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

        }

        public async Task<BasketToReturnDto> CreateOrUpdatePaymentIntent(string basketId)
        {


            // check basket
            var basket = await _basketService.GetBasketByIdAsync(basketId);
            if (basket is null) return null;

            // check price
            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var course = await _unitOfWork.Repository<Course, int>().GetByIdAsync(item.CourseId);
                    if (course.Price != item.Price)
                        item.Price = course.Price;
                }
            }

            // calculate Total
            var total = basket.Items.Sum(I => I.Price);


            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // create 
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(total * 100),
                    PaymentMethodTypes = new List<string>() { "card" },
                    Currency = "usd",
                    Metadata = new Dictionary<string, string>
                    {
                        { "basket_id", basketId }
                    }
                };

                // create payment using stripe
                var paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // update 

                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(total * 100),
                };

                // update payment using stripe
                var paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }

            //update basket
            var entity = _mapper.Map<BasketDto>(basket);
            basket = await _basketService.CreateOrUpdateBasketAsync(entity);

            if (basket is null) return null;

            return basket;
        }

        public async Task<OrderToReturnDto> HandlePaymentIntentSucceeded(string paymentIntentId , int studentId )
        {

            var spec = new OrderPaymentIntentSpecifications(paymentIntentId);

            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);

            if (order is null) throw new InvalidOperationException($"No order found for payment intent: {paymentIntentId}");

            order.Status = OrderStatus.PaymentReceived;

            _unitOfWork.Repository<Order, int>().Update(order);

            await _studentService.EnrollStudentInPaidCoursesAsync(order.Id, order.BuyerMail, studentId);

            var res = await _unitOfWork.CompleteAsync();


            return res > 0 ? _mapper.Map<OrderToReturnDto>(order) :
                throw new Exception("Failed to update the order status after payment");

        }

        public async Task<OrderToReturnDto> HandlePaymentIntentFailed(string paymentIntentId)
        {
            var spec = new OrderPaymentIntentSpecifications(paymentIntentId);

            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);

            if (order is null) throw new InvalidOperationException("Failed to update the order status after payment failure");


            order.Status = OrderStatus.Failed;

            _unitOfWork.Repository<Order, int>().Update(order);

            var res = await _unitOfWork.CompleteAsync();


            return res > 0 ? _mapper.Map<OrderToReturnDto>(order) :
                throw new InvalidOperationException("Failed to update the order status after payment failure");
        }

        public async Task<OrderToReturnDto> GetOrderByPaymentIntentId(string paymentIntentId)
        {
            var spec = new OrderPaymentIntentSpecifications(paymentIntentId);
            var order =  await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
