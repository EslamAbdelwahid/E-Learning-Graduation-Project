using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
using E_Learning.GraduationProject.Core.Dtos.ToggleFavorites;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{studentId}/enrolledcourses")]
        public async Task<ActionResult<IEnumerable<EnrolledCourseDto>>> GetEnrolledCourses(int studentId)
        {
            var enrolledCourses = await _studentService.GetEnrolledCoursesAsync(studentId);

            if (!enrolledCourses.Any())
            {
                return NotFound("No enrolled courses found for this student.");
            }

            return Ok(enrolledCourses);
        }

        [HttpGet("favorite-courses/{studentId}")]
        public async Task<IActionResult> GetFavoriteCourses(int studentId)
        {
            var result = await _studentService.GetFavoriteCoursesOfStudent(studentId);

            if (result is null )
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, $"No favorite courses found for student ID {studentId}"));

            return Ok(result);
        }

        [HttpPost("favorites/toggle")]
        public async Task<IActionResult> ToggleFavorite(ToggleFavoriteDto dto )
        {
            var result = await _studentService.ToggleFavoriteCourse(dto.StudentId, dto.CourseId);
            return Ok(new { IsFavorite = result });
        }

        [HttpGet("{studentId}/favorites/{courseId}")]
        public async Task<IActionResult> CheckFavoriteStatus(int studentId, int courseId)
        {
            var isFavorite = await _studentService.IsCourseFavorite(studentId, courseId);
            return Ok(new { IsFavorite = isFavorite });
        }

        [HttpPost("{studentId}/enroll")]
        public async Task<IActionResult> EnrollInCourse(int studentId, [FromBody] EnrollCourseDto dto)
        {
            var result = await _studentService.EnrollInCourseAsync(studentId, dto);

            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Failed to enroll. The course may not exist or you are already enrolled."
                });
            }

            return Ok(result);
        }

        [HttpPost("{studentId}/enroll/free/{courseId}")]
        public async Task<ActionResult<EnrolledCourseDto>> EnrollInFreeCourse(int studentId, int courseId)
        {

            var result = await _studentService.EnrollStudentInFreeCourseAsync(studentId, courseId);
            return Ok(result);

        }

        [HttpGet("{studentId}/HasAccess/{courseId}")]
        public async Task<ActionResult<bool>> HasCourseAccess(int studentId, int courseId)
        {
            var hasAccess = await _studentService.HasCourseAccessAsync(studentId, courseId);
            return Ok(hasAccess);
        }
    }
}
