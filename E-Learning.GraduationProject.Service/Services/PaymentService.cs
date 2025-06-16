using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Service.Contract;
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

        public PaymentService(
            IBasketService basketService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
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
                    Currency = "usd"
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
                    Amount = (long)(total  * 100),
                };

                // update payment using stripe
                var paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }

            //update basket
            var entity =  _mapper.Map<BasketDto>(basket);
            basket = await _basketService.CreateOrUpdateBasketAsync(entity);

            if (basket is null) return null;

            return basket;
        }
    }
}
