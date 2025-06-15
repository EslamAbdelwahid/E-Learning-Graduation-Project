using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.BasketItems;
using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Entities.Baskets;
using E_Learning.GraduationProject.Core.Repository.Contract;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public BasketService(
            IBasketRepository basketRepository,
            IMapper mapper,
            ICourseService courseService,
            IUserService userService
            )
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _courseService = courseService;
            _userService = userService;
        }

        public async Task<BasketToReturnDto> GetBasketByIdAsync(string? basketId)
        {
         
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null)
            {
                basket = new Basket() { Id = basketId };
            }

            return _mapper.Map<BasketToReturnDto>(basket);
        }

        public async Task<BasketToReturnDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            if (basket is null) return null;
            var entity = _mapper.Map<Basket>( basket);

            var user = await _userService.GetUserByIdAsync(basket.UserId);

            if (user is null) throw new KeyNotFoundException($"Invalid User ID : {basket.UserId} "); 

            foreach (var item in entity.Items)
            {
                var course = await _courseService.GetCourseByIdAsync(item.CourseId);
                if (course is null)
                    throw new KeyNotFoundException($"course with ID : {item.CourseId} not found");

                item.CourseName = course.Title;
                item.Price = course.Price;
            }


            var ret = await _basketRepository.SetBasketAsync(entity);

            return _mapper.Map<BasketToReturnDto>(ret);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }

       
    }
}
