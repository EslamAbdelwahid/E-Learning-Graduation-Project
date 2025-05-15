using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Resources;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.ConceptResources;
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
        public async Task<ActionResult<IEnumerable<ConceptResourceToReturn>>> GetAllResources([FromQuery]ConceptResourceParames parames)
        {
            var spec = new ConceptResourceSpecifications(parames);
            var resources = await _resourceService.GetAllResourcesAsync(parames);
            if (resources is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConceptResourceToReturn>> GetResourceById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var resource = await _resourceService.GetResourceByIdAsync(id.Value);
            if (resource is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(resource);
        }

        //not tested yet
        [HttpGet("{languageId}/Resources")]
        public async Task<ActionResult<IEnumerable<ConceptResourceToReturn>>> GetAllResourcesForSpecificLanguage(int? languageId)
        {
            if (languageId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var resources = await _resourceService.GetAllResourcesForSpecificLanguageAsync(languageId.Value);

            if (resources is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(resources);
        }

        [HttpPost]
        public async Task<ActionResult<ConceptResourceToReturn>> CreateResource(ConceptResourceDto model)
        {
            var resource = await _resourceService.CreateResourceAsync(model);

            if (resource is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(resource);

        }

        [HttpDelete("{resourceId}")]
        public async Task<IActionResult> DeleteResource(int? resourceId)
        {
            if (resourceId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var result = await _resourceService.DeleteResourceAsync(resourceId.Value);

            if (result == 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<ConceptResourceToReturn>> UpdateResource(ConceptResourceDto model)
        {
            var resource = await _resourceService.UpdateResourceAsync(model);

            if (resource is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(resource);

        }



    }
}
