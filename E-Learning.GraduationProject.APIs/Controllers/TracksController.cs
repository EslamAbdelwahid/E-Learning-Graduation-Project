using AutoMapper;
using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Tracks;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITrackService trackService;
        private readonly IMapper mapper;

        public TracksController(ITrackService trackService, IMapper mapper)
        {
            this.trackService = trackService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackResponseDto>>> GetAllTracks()
        {
            var tracks = await trackService.GetAllTracksWithSpecAsync();
            if (tracks is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            var tracksDto = mapper.Map<IEnumerable<TrackResponseDto>>(tracks);
            return Ok(tracksDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackResponseDto>> GetTrackById(int id)
        {
            var track = await trackService.GetTrackByIdWithSpecAsync(id);
            if (track is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(mapper.Map<TrackResponseDto>(track));
        }
        [HttpPost]
        public async Task<ActionResult<CreateTrackDto>> CreateTrack(CreateTrackDto trackDto)
        {
            if(trackDto is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var track = await trackService.CreateTrackAsync(mapper.Map<Track>(trackDto));
            if (track is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(trackDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CreateTrackDto>> UpdateTrack(int id, UpdateTrackDto trackDto)
        {
            if(id != trackDto.Id)
            {
                return BadRequest(new ApiErrorResponse(400, "ID mismatch"));
            }
            var track = await trackService.UpdateTrackAsync(mapper.Map<Track>(trackDto));
            if (track is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(trackDto);
        }

        [HttpDelete("{trackId}")]
        public async Task<ActionResult<TrackResponseDto>> DeleteTrack(int trackId)
        {
            var track = await trackService.RemoveTrackAsync(trackId);
            if(track is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(mapper.Map<TrackResponseDto>(track));
        }
    }
}
