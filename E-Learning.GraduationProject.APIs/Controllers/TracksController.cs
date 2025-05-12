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

        public TracksController(ITrackService trackService)
        {
            this.trackService = trackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTracks()
        {
            var tracks = await trackService.GetAllTracksWithSpecAsync();
            if (tracks is null) return NotFound();
            return Ok(tracks);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackById(int? id)
        {
            if(id is null) return BadRequest();
            var track = await trackService.GetTrackByIdWithSpecAsync(id.Value);
            if (track is null) return NotFound();
            return Ok(track);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrack(Track track)
        {
            if(track is null) return BadRequest();
            var added = await trackService.CreateTrackAsync(track);
            if (!added) return BadRequest();
            return Ok(track);
        }

        [HttpDelete("{trackId}")]
        public async Task<IActionResult> DeleteTrack(int trackId)
        {
            var res = await trackService.RemoveTrackAsync(trackId);
            if(res == false) return BadRequest();
            return Ok(true);
        }
    }
}
