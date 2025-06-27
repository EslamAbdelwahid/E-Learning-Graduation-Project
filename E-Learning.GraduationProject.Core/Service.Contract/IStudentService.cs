using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IStudentService
    {
        Task<IEnumerable<EnrolledCourseDto>?> GetEnrolledCoursesAsync(int studentId);
        Task<IEnumerable<EnrolledCourseDto>?> GetFavoriteCoursesOfStudent(int studentId);
        Task<bool> EnrollStudentInPaidCoursesAsync(int orderId, string buyerEmail, int studentId);
        Task<EnrolledCourseDto> EnrollStudentInFreeCourseAsync(int studentId, int courseId);
        Task<bool> HasCourseAccessAsync(int studentId, int courseId);
        Task<EnrolledCourseDto?> EnrollInCourseAsync(int studentId, EnrollCourseDto dto);
        Task<bool> ToggleFavoriteCourse(int studentId, int courseId);
        Task<bool> IsCourseFavorite(int studentId, int courseId);
    }
}
