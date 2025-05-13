using E_Learning.GraduationProject.Core.Dtos.Resources;
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
        private readonly IResourceService _resourceService;

        public ConceptResourceController(
            IResourceService resourceService
            )
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConceptResourceToReturn>>> GetAllResources()
        {
            var resources = await _resourceService.GetAllResourcesAsync();
            if (resources is null) return NotFound();

            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConceptResourceToReturn>> GetResourceById(int? id)
        {
            if (id is null) return BadRequest();

            var resource = await _resourceService.GetResourceByIdAsync(id.Value);
            if (resource is null) return NotFound();

            return Ok(resource);
        }

        [HttpGet("{conceptId}/Resources")]
        public async Task<ActionResult<IEnumerable<ConceptResourceToReturn>>> GetAllResourcesForSpecificConcept(int? conceptId)
        {
            if (conceptId is null) return BadRequest();

            var resources = await _resourceService.GetAllResourcesForSpecificConceptAsync(conceptId.Value);

            if (resources is null) return NotFound();

            return Ok(resources);
        }

        [HttpPost]
        public async Task<ActionResult<ConceptResourceToReturn>> CreateResource(ConceptResourceDto model)
        {
            var resource = await _resourceService.CreateResourceAsync(model);

            if (resource is null) return BadRequest();

            return Ok(resource);

        }

        [HttpDelete("{resourceId}")]
        public async Task<IActionResult> DeleteResource(int? resourceId)
        {
            if (resourceId is null) return BadRequest();

            var result = await _resourceService.DeleteResourceAsync(resourceId.Value);

            if (result == 0) return BadRequest("Failed to Delete");

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<ConceptResourceToReturn>> UpdateResource(ConceptResourceDto model)
        {
            var resource = await _resourceService.UpdateResourceAsync(model);

            if (resource is null) return BadRequest();

            return Ok(resource);

        }



    }
}
