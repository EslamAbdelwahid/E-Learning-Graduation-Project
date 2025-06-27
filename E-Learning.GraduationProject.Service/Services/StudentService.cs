using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Entities.Orders;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Orders;
using E_Learning.GraduationProject.Core.Specifications.StudentFavoriteCourses;
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
        private readonly IMapper _mapper;

        public StudentService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<EnrolledCourseDto>?> GetEnrolledCoursesAsync(int studentId)
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

        public async Task<IEnumerable<EnrolledCourseDto>?> GetFavoriteCoursesOfStudent(int studentId )
        {
            var spec = new StudentFavoriteCoursesSpec(studentId);

            var favCourses = await _unitOfWork.Repository<StudentCourseFavorite, int>().GetAllWithSpecAsync(spec);

            if (favCourses is null) return null;

            var enrolledCourses = favCourses.Select(sp => new EnrolledCourseDto
            {
                CourseId = sp.Course.Id,
                Title = sp.Course.Title,
                Description = sp.Course.Description,
                Price = sp.Course.Price,
                ThumbnailUrl = sp.Course.ThumbnailUrl,
                IsPublished = sp.Course.IsPublished,
                IsFavorite = true
            });

            return enrolledCourses; 
        }

        public async Task<bool> EnrollStudentInPaidCoursesAsync(int orderId, string buyerEmail, int studentId)
        {

            var spec = new OrderSpecifications(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);

            if (order == null)
            {
                throw new InvalidOperationException
                    ($"Order {orderId} not found or does not belong to Student mail {buyerEmail}");
            }

            // Verify order is in a paid status
            if (order.Status != OrderStatus.PaymentReceived)
            {
                throw new InvalidOperationException
                    ($"Order {orderId} is not in a paid status. Current status: {order.Status}");
            }



            foreach (var orderItem in order.OrderItems)
            {
                var courseId = orderItem.Course.CourseId;

                // Check for existing enrollment
                var enrollmentSpec = new StudentEnrollmentsWithCourseSpec(studentId, courseId);
                var existingEnrollment = await _unitOfWork.Repository<StudentProgress, int>().GetWithSpecAsync(enrollmentSpec);

                if (existingEnrollment == null)
                {

                    // Create new enrollment linked to the paid order
                    var studentProgress = new StudentProgress
                    {
                        StudentId = studentId,
                        CourseId = courseId,
                        OrderId = orderId,
                        IsCompleted = false,
                        Status = ProgressStatus.InProgress,
                    };

                    await _unitOfWork.Repository<StudentProgress, int>().AddAsync(studentProgress);

                }
                else
                {
                    throw new InvalidOperationException($"Student {studentId} already enrolled in Course {courseId}");
                }
            }

            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? true : false;

        }
        public async Task<EnrolledCourseDto> EnrollStudentInFreeCourseAsync(int studentId, int courseId)
        {
           
            var course = await _unitOfWork.Repository<Course, int>().GetByIdAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course {courseId} not found");

            // Ensure course is free
            if (course.Price > 0)
                throw new InvalidOperationException($"Course {courseId} requires payment");

            var spec = new StudentEnrollmentsWithCourseSpec(studentId, courseId);
            var existing = await _unitOfWork.Repository<StudentProgress, int>().GetWithSpecAsync(spec);

            if (existing != null)
                return _mapper.Map<EnrolledCourseDto>(existing);

            // Create new enrollment
            var progress = new StudentProgress
            {
                StudentId = studentId,
                CourseId = courseId,
                OrderId = null, // Explicitly null for free courses
                ProgressPercentage = 0,
                IsCompleted = false,
                EnrolledAt = DateTime.UtcNow,
                Status = ProgressStatus.InProgress
            };

            await _unitOfWork.Repository<StudentProgress, int>().AddAsync(progress);
            await _unitOfWork.CompleteAsync();

            return new EnrolledCourseDto() 
            { 
                CourseId = progress.CourseId,
                Description = progress.Course.Description,
                Title = progress.Course.Title,
                Price = course.Price,
                ThumbnailUrl = course.ThumbnailUrl,
                IsPublished = course.IsPublished
            };

        }
        public async Task<bool> HasCourseAccessAsync(int studentId, int courseId)
        {
            var spec = new StudentEnrollmentsWithCourseSpec(studentId, courseId);
            var enrollment = await _unitOfWork.Repository<StudentProgress, int>().GetWithSpecAsync(spec);

            if (enrollment == null)
                return false;

            // For paid courses, verify the order is paid
            if (enrollment.OrderId.HasValue)
            {
                var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(enrollment.OrderId.Value);
                return order?.Status == OrderStatus.PaymentReceived;
            }

            // Free courses always have access
            return true;
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

        public async Task<bool> ToggleFavoriteCourse(int studentId, int courseId)
        {
            // Check if the course exists
            var courseExists = await _unitOfWork.Repository<Course, int>().GetByIdAsync(courseId);
            if (courseExists == null ) return false;

            // Check if already favorite
            var spec = new StudentFavoriteCoursesSpec(studentId ,courseId);
            var existingFavorite = await _unitOfWork.Repository<StudentCourseFavorite, int>().GetWithSpecAsync(spec);

            if (existingFavorite != null)
            {
                // Remove from favorites
                _unitOfWork.Repository<StudentCourseFavorite, int>().Delete(existingFavorite);
                var res = await _unitOfWork.CompleteAsync();
                
                return res > 0 ? false : throw new InvalidOperationException("there is a problem while removing favorites");
            }
            else
            {
                // Add to favorites
                var favorite = new StudentCourseFavorite
                {
                    StudentId = studentId,
                    CourseId = courseId,
                };
                await _unitOfWork.Repository<StudentCourseFavorite, int>().AddAsync(favorite);
                var res = await _unitOfWork.CompleteAsync();

                return res > 0 ? true : throw new InvalidOperationException("there is a problem while adding favorites");
            }
        }

        public async Task<bool> IsCourseFavorite(int studentId, int courseId)
        {
            var spec = new StudentFavoriteCoursesSpec(studentId, courseId);
            var fav = await _unitOfWork.Repository<StudentCourseFavorite, int>().GetWithSpecAsync(spec);

            return fav == null ? false: true;
        }
    }
}
