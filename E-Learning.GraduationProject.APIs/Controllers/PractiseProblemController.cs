using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.PractiseProblems;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PractiseProblemController : ControllerBase
    {
        private readonly IPractiseProblemService _practiseProblemService;

        public PractiseProblemController(
            IPractiseProblemService practiseProblemService
            )
        {
            _practiseProblemService = practiseProblemService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PractiseProblemToReturnDto>>> GetAllPractiseProblem()
        {
            var problems = await _practiseProblemService.GetAllPractiseProblemAsync();
            if (problems is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(problems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PractiseProblemToReturnDto>> GetPractiseProblemById(int id)
        {

            var problem = await _practiseProblemService.GetPractiseProblemByIdAsync(id);

            if (problem is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(problem);
        }

        [HttpPost]
        public async Task<ActionResult<PractiseProblemToReturnDto>> CreatePractiseProblem(PractiseProblemDto model)
        {

            var problem = await _practiseProblemService.CreatePractiseProblemAsync(model);

            if (problem is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Failed to Create"));

            return Ok(problem);
        }
        [HttpPut]
        public async Task<ActionResult<PractiseProblemToReturnDto>> UpdatePractiseProblem(PractiseProblemDto model)
        {

            var problem = await _practiseProblemService.UpdatePractiseProblemAsync(model);

            if (problem is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Failed to update"));

            return Ok(problem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePractiseProblem(int id)
        {

            var result = await _practiseProblemService.DeletePractiseProblemAsync(id);

            if (result == 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Failed to delete"));

            return Ok();
        }


    }
}
