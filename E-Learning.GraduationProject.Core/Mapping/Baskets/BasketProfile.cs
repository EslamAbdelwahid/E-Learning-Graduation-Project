using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.BasketItems;
using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            //basket
            CreateMap<BasketDto, Basket>();
            CreateMap<Basket, BasketToReturnDto>();
            CreateMap<BasketToReturnDto, BasketDto>();

            //item
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<BasketItem, BasketItemToReturnDto>();
            CreateMap<BasketItemToReturnDto, BasketItemDto>();

        }
    }
}
