using AutoMapper;
using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.StepResources;
using E_Learning.GraduationProject.Core.Dtos.TackSteps;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.StepResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepResourcesController : ControllerBase
    {
        private readonly IStepResourceService resourceService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public StepResourcesController(IStepResourceService resourceService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.resourceService = resourceService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<ActionResult<CreateStepResourceDto>> CreateStepResource(CreateStepResourceDto stepResourceDto)
        {
            var stepResource = await resourceService.CreateResourceAsync(mapper.Map<StepResource>(stepResourceDto));
            if (stepResource is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(stepResourceDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateStepResourceDto>> UpdateStepResource(int id, UpdateStepResourceDto stepResourceDto)
        {
            if (id != stepResourceDto.Id)
            {
                return BadRequest(new ApiErrorResponse(400, "ID mismatch"));
            }
            var stepResource = await resourceService.UpdateResourceAsync(mapper.Map<StepResource>(stepResourceDto));
            if (stepResource is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Updates failed"));
            return Ok(stepResourceDto);
        }

        [HttpDelete]
        public async Task<ActionResult<StepResourceResponseDto>> DeleteStepResource([FromQuery]int stepId, [FromQuery] int resourceId)
        {
            var stepResource = await resourceService.DeleteResourceAsync(stepId, resourceId);
            if (stepResource is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(mapper.Map<StepResourceResponseDto>(stepResource));
        }

        [HttpGet("all-resources-for-step")]
        public async Task<ActionResult<PaginationResponseToReturn<StepResourceResponseDto>>> GetAllResourcesForSpecificStep([FromQuery] StepResourceSpecParams specParams)
        {
            // Using the new service method that handles pagination internally
            var paginatedResponse = await resourceService.GetPaginatedResourcesForStepAsync(specParams);
            if (paginatedResponse is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(paginatedResponse);
        }

        [HttpGet]
        public async Task<ActionResult<StepResourceResponseDto>> GetResource([FromQuery] int stepId,[FromQuery] int resourceId)
        {
            var stepResource = await resourceService.GetResourcesWithSpecAsync(stepId, resourceId);
            if (stepResource is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(mapper.Map<StepResourceResponseDto>(stepResource));
        }
    }
}
