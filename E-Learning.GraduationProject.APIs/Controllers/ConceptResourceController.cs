using E_Learning.GraduationProject.Core.Dtos;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptResourceController : ControllerBase
    {
        private readonly IConceptResourceService _resourceService;

        public ConceptResourceController(IConceptResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        // not tested yet
        [HttpGet("AllResources")]
        public async Task<ActionResult<IEnumerable<ConceptResourceDto>>> GetAllResources()
        {
            var resources = await _resourceService.GetAllResourcesAsync();
            if (resources is null) return NotFound();

            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConceptResourceDto>> GetResourceById(int? id)
        {
            if (id is null) return BadRequest();

            var resource = await _resourceService.GetResourceByIdAsync(id.Value);
            if (resource is null) return NotFound();

            return Ok(resource);
        }

        [HttpGet("{conceptId}/Resources")]
        public async Task<ActionResult<IEnumerable<ConceptResourceDto>>> GetAllResourcesForSpecificConcept(int? conceptId)
        {
            if (conceptId is null) return BadRequest();

            var resources = await _resourceService.GetAllResourcesForSpecificConceptAsync(conceptId.Value);

            if (resources is null) return NotFound();

            return Ok(resources);
        }

        [HttpPost]
        public async Task<ActionResult<ConceptResourceDto>> CreateResource(ConceptResourceDto model)
        {
            var resource = await _resourceService.CreateResourceAsync(model);

            if (resource is null) return BadRequest();

            return Ok(resource);

        }

        [HttpDelete("{resourceId}")]
        public async Task<IActionResult> DeleteResource(int? resourceId)
        {
            if (resourceId is null) return BadRequest();
            var resource = await _resourceService.GetResourceByIdAsync(resourceId.Value);
            if (resource is null) return NotFound();
            var result = await _resourceService.DeleteResourceAsync(resource);
            if (result == 0) return BadRequest("the resource not deleted");
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<ConceptResourceDto>> UpdateResource(ConceptResourceDto model)
        {
            var resource = await _resourceService.UpdateResourceAsync(model);

            if (resource is null) return BadRequest();

            return Ok(resource);

        }



    }
}
