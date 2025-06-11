using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Students;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Students
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentToReturnDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address));
                

        }
    }
}
