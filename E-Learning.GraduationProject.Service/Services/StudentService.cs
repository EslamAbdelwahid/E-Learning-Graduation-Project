using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EnrolledCourseDto>> GetEnrolledCoursesAsync(int studentId)
        {
            var spec = new StudentEnrollmentsWithCourseSpec(studentId);

            var enrollments = await _unitOfWork.Repository<StudentProgress, int>().GetAllWithSpecAsync(spec);

            var enrolledCourses = enrollments.Select(sp => new EnrolledCourseDto
            {
                CourseId = sp.Course.Id,
                Title = sp.Course.Title,
                Description = sp.Course.Description,
                Price = sp.Course.Price,
                ThumbnailUrl = sp.Course.ThumbnailUrl,
                IsPublished = sp.Course.IsPublished,

            });

            return enrolledCourses;
        }

        public async Task<EnrolledCourseDto?> EnrollInCourseAsync(int studentId, EnrollCourseDto dto)
        {
            var course = await _unitOfWork.Repository<Course, int>().GetByIdAsync(dto.CourseId);
            if (course == null)
            {
                return null; 
            }

            var existingEnrollment = await _unitOfWork.Repository<StudentProgress, int>()
                .GetAllAsync();

            if (existingEnrollment.Any(ep => ep.StudentId == studentId && ep.CourseId == dto.CourseId))
            {
                return null; 
            }

            var studentProgress = new StudentProgress
            {
                StudentId = studentId,
                CourseId = dto.CourseId,
                ProgressPercentage = 0,
                IsCompleted = false,
                EnrolledAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<StudentProgress, int>().AddAsync(studentProgress);
            await _unitOfWork.CompleteAsync();

            return new EnrolledCourseDto
            {
                CourseId = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                ThumbnailUrl = course.ThumbnailUrl,
                IsPublished = course.IsPublished
            };
        }
    }
}
