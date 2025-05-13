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
        public async Task<IActionResult> GetAllTracks()
        {
            var tracks = await trackService.GetAllTracksWithSpecAsync();
            if (tracks is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            var tracksDto = mapper.Map<IEnumerable<TrackResponseDto>>(tracks);
            return Ok(tracksDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackById(int? id)
        {
            if(id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var track = await trackService.GetTrackByIdWithSpecAsync(id.Value);
            if (track is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(mapper.Map<TrackResponseDto>(track));
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrack(CreateTrackDto trackDto)
        {
            if(trackDto is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var added = await trackService.CreateTrackAsync(mapper.Map<Track>(trackDto));
            if (!added) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(trackDto);
        }

        [HttpDelete("{trackId}")]
        public async Task<IActionResult> DeleteTrack(int trackId)
        {
            var res = await trackService.RemoveTrackAsync(trackId);
            if(res == false) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(true);
        }
    }
}
