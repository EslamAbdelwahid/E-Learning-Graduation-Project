using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Instructors
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<Instructor, InstructorToReturnDto>()
                .ForMember(D => D.Address, options => options.MapFrom(S => S.User.Address))
                .ForMember(D => D.FullName, options => options.MapFrom(S => S.User.FirstName + ' ' + S.User.LastName))
                .ForMember(D => D.FirstName, options => options.MapFrom(S => S.User.FirstName))
                .ForMember(D => D.LastName, options => options.MapFrom(S => S.User.LastName))
                .ForMember(D => D.UserId , options => options.MapFrom(S => S.User.Id))
                .ForMember(D => D.Email , options => options.MapFrom(S => S.User.Email));

            CreateMap<InstructorDto, Instructor>()
                .ForPath(D => D.User.FirstName, options => options.MapFrom(S => S.FirstName))
                .ForPath(D => D.User.LastName, options => options.MapFrom(S => S.LastName));
            

        }
    }
}
