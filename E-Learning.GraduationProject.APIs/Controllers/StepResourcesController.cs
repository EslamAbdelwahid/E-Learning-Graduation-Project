using AutoMapper;
using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.StepResources;
using E_Learning.GraduationProject.Core.Dtos.TackSteps;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
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

        public StepResourcesController(IStepResourceService resourceService, IMapper mapper)
        {
            this.resourceService = resourceService;
            this.mapper = mapper;
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<StepResourceResponseDto>> UpdateStepResource(int id)
        {
            var stepResource = await resourceService.DeleteResourceAsync(id);
            if (stepResource is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(mapper.Map<StepResourceResponseDto>(stepResource));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StepResourceResponseDto>>> GetAllResources()
        {
            var resources = await resourceService.GetAllResourcesWithSpecAsync();
            if (resources is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(mapper.Map<IEnumerable<StepResourceResponseDto>>(resources));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StepResourceResponseDto>> GetResourceById(int id)
        {
            var stepResource = await resourceService.GetResourcesByIdWithSpecAsync(id);
            if (stepResource is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(mapper.Map<StepResourceResponseDto>(stepResource));
        }
    }
}
