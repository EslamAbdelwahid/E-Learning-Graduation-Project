using E_Learning.GraduationProject.APIs.Attributes;
using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Courses;
using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Courses;
using E_Learning.GraduationProject.Core.Specifications.Instructors;
using E_Learning.GraduationProject.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(
            ICourseService courseService
            )
        {
            _courseService = courseService;
        }

        [HttpGet]
       // [Cached(300)] // 5 min
        public async Task<ActionResult<PaginationResponseToReturn<CourseToReturnDto>>> GetAllCourses([FromQuery] CourseParams specParams)
        {
            var Courses = await _courseService.GetAllCoursesAsync(specParams);
            if (Courses is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(Courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseToReturnDto>> GetCourseById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var course = await _courseService.GetCourseByIdAsync(id.Value);
            if (course is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<CourseToReturnDto>> CreateCourse(CourseDto model)
        {
            var course = await _courseService.CreateCourseAsync(model);
            if (course is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(course);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CourseToReturnDto>> UpdateCourse(int? id , CourseDto model)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var course = await _courseService.UpdateCourseAsync(id.Value , model);
            if (course is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(course);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var res = await _courseService.DeleteCourseAsync(id.Value);
            if (res == 0) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return NoContent();
        }
    }
}
