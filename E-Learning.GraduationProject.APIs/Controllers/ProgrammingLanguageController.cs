using E_Learning.GraduationProject.Core.Dtos;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguageController : ControllerBase
    {
        private readonly IProgrammingLanguageService _programmingLanguageService;

        public ProgrammingLanguageController(
            IProgrammingLanguageService programmingLanguageService
            )
        {
            _programmingLanguageService = programmingLanguageService;
        }

        [HttpGet("ProgrammingLanguages")]
        public async Task<ActionResult<IEnumerable<ProgrammingLanguageDto>>> GetAllProgrammingLanguages()
        {
            var languages = await _programmingLanguageService.GetAllProgrammingLanguageAsync();
            if (languages is null) return NotFound();

            return Ok(languages);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgrammingLanguageDto>> GetProgrammingLanguageById(int? id)
        {
            if (id is null) return BadRequest();
            var programmingLanguage = await _programmingLanguageService.GetProgrammingLanguageByIdAsync(id.Value);
            if (programmingLanguage is null) return NotFound();
            return Ok(programmingLanguage);

        }

        [HttpPost]
        public async Task<ActionResult<ProgrammingLanguageDto>> CreateProgrammingLanguage(ProgrammingLanguageDto model)
        {
            var programmingLanguage = await _programmingLanguageService.CreateProgrammingLanguageAsync(model);
            if (programmingLanguage is null) return BadRequest("Failed to create");
            return Ok(programmingLanguage);

        }
        [HttpPut]
        public async Task<ActionResult<ProgrammingLanguageDto>> UpdateProgrammingLanguage(ProgrammingLanguageDto model)
        {
            var programmingLanguage = await _programmingLanguageService.UpdateProgrammingLanguageAsync(model);
            if (programmingLanguage is null) return BadRequest("Failed to update");
            return Ok(programmingLanguage);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProgrammingLanguageDto>> DeleteProgrammingLanguage(int? id)
        {
            if (id is null) return BadRequest();

            var entity = await _programmingLanguageService.GetProgrammingLanguageByIdAsync(id.Value);

            if (entity is null) return NotFound();

            var result = await _programmingLanguageService.DeleteAsync(entity);

            if (result == 0 ) return BadRequest("Failed to delete");

            return Ok();
        }
    }
}
