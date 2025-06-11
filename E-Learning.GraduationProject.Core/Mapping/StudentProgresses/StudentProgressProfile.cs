using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.StudentProgresses
{
    public class StudentProgressProfile : Profile
    {
        public StudentProgressProfile()
        {

            CreateMap<StudentProgress, StudentProgressToReturnDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.User.FirstName + ' ' + src.Student.User.LastName))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.CompletionDate, opt => opt.MapFrom(src => src.CompletionDate));
        }
    }
}
