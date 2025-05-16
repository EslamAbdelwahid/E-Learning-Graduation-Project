using AutoMapper;
using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.TackSteps;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackStepsController : ControllerBase
    {
        private readonly ITrackStepService stepService;
        private readonly IMapper mapper;

        public TrackStepsController(ITrackStepService stepService, IMapper mapper)
        {
            this.stepService = stepService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateTrackStepDto>> CreateTrackStep(CreateTrackStepDto trackStepDto)
        {
            var trackStep = await stepService.CreateTrackStepAsync(mapper.Map<TrackStep>(trackStepDto));
            if (trackStep is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(trackStepDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateTrackStepDto>> UpdateStep(int id, UpdateTrackStepDto trackStepDto)
        {
            if(id != trackStepDto.TrackStepId)
            {
                return BadRequest(new ApiErrorResponse(400, "ID mismatch"));
            }
            var trackStep = await stepService.UpdateTrackStepAsync(mapper.Map<TrackStep>(trackStepDto));
            if (trackStep is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Updates failed"));
            return Ok(trackStepDto);
        }

        [HttpGet("{trackId}")]
        public async Task<ActionResult<IEnumerable<TrackStepResponseDto>>> GetAllStepsForSpecificTrack(int trackId)
        {
            var trackSteps = await stepService.GetAllStepsForASpecificTrackWithSpecAsync(trackId);
            if (trackSteps is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(mapper.Map<IEnumerable<TrackStepResponseDto>>(trackSteps));
        }
        [HttpGet]
        public async Task<ActionResult<TrackStepResponseDto>> GetStep([FromQuery]int trackId, [FromQuery] int stepId)
        {
            var trackStep = await stepService.GetStepWithSpecAsync(trackId, stepId);
            if (trackStep is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(mapper.Map<TrackStepResponseDto>(trackStep));
        }
        [HttpDelete]
        public async Task<ActionResult<TrackStepResponseDto>> DeleteStep([FromQuery] int trackId, [FromQuery] int stepId)
        {
            var trackStep = await stepService.DeleteTrackStepAsync(trackId, stepId);
            if (trackStep is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(mapper.Map<TrackStepResponseDto>(trackStep));
        }
    }
}
