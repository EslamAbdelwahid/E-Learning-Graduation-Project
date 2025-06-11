using E_Learning.GraduationProject.Core.Dtos.Courses;
using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Dtos.Students;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Specifications.Courses;
using E_Learning.GraduationProject.Core.Specifications.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface ICourseService
    {
        public Task<PaginationResponseToReturn<CourseToReturnDto>?> GetAllCoursesAsync(CourseParams specParams);
        public Task<CourseToReturnDto?> GetCourseByIdAsync(int id);
        public Task<CourseToReturnDto?> CreateCourseAsync(CourseDto model);
        public Task<CourseToReturnDto?> UpdateCourseAsync(int id ,  CourseDto model);
        public Task<int> DeleteCourseAsync(int id);

        
        
    }
}
