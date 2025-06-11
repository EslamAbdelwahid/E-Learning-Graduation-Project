using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Instructors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(
            IInstructorService instructorService
            )
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponseToReturn<InstructorToReturnDto>>> GetAllInstructors([FromQuery]InstructorParams specParams)
        {
            var instructors = await _instructorService.GetAllInstructorsAsync(specParams);
            if (instructors is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorToReturnDto>> GetInstructorById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var instructor = await _instructorService.GetInstructorByIdAsync(id.Value);
            if (instructor is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(instructor);
        }

        [HttpPost]
        public async Task<ActionResult<InstructorToReturnDto>> CreateInstructor(InstructorDto model)
        {
            var instructor = await _instructorService.CreateInstructorAsync(model);
            if (instructor is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(instructor);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<InstructorToReturnDto>> UpdateInstructor(int? id  , InstructorDto model)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var instructor = await _instructorService.UpdateInstructorAsync(id.Value , model);
            if (instructor is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(instructor);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var res = await _instructorService.DeleteInstructorAsync(id.Value);
            if (res == 0 ) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return NoContent();
        }

    }
}
