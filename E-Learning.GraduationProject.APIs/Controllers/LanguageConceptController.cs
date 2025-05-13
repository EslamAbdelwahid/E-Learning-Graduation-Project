using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.LanguageConcepts;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageConceptController : ControllerBase
    {
        private readonly IConceptService _conceptService;

        public LanguageConceptController(
            IConceptService conceptService
            )
        {
            _conceptService = conceptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConcepts()
        {
            var result = await _conceptService.GetAllConceptsWithSpecAsync();
            if (result is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConceptById(int? id )
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var result = await _conceptService.GetConceptByIdWithSpec(id.Value);
            if (result is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConcept(LanguageConceptDto model)
        {
            var result = await _conceptService.CreateConcept(model);

            if (result is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConcept(LanguageConceptDto model)
        {
            var result = await _conceptService.UpdateConcept(model);

            if (result is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcept(int?  id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var result = await _conceptService.DeleteConcept(id.Value);

            if (result == 0 ) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok();
        }


    }
}
