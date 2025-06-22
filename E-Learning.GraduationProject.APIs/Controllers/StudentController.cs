using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
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
            this._studentService = studentService;
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
    }
}
