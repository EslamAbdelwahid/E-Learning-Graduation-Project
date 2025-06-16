using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Orders;
using E_Learning.GraduationProject.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderToReturnDto>();
            CreateMap<OrderItem, OrderItemToReturnDto>()
                .ForMember(S => S.CourseId , opt => opt.MapFrom(D => D.Course.CourseId))
                .ForMember(S => S.CourseName , opt => opt.MapFrom(D => D.Course.CourseName))
                .ForMember(S => S.PictureUrl, opt => opt.MapFrom(D => D.Course.PictureUrl));
            
        }
    }
}
