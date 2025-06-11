using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Courses;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Courses
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseToReturnDto>();
            CreateMap<CourseDto , Course>() 
                .ForMember(D => D.ProgrammingLanguage ,option => option.Ignore())
                .ForMember(D => D.Track ,option => option.Ignore());
        }
    }
}
