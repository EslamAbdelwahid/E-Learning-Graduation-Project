using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications;
using E_Learning.GraduationProject.Core.Specifications.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class TrackService : ITrackService
    {
        private readonly IUnitOfWork unitOfWork;

        public TrackService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Track?> CreateTrackAsync(Track track)
        {
            await unitOfWork.Repository<Track, int>().AddAsync(track);
            var res = await unitOfWork.CompleteAsync();
            return res > 0 ? track : null;
        }


        public async Task<IEnumerable<Track>> GetAllTracksWithSpecAsync()
        {
            var spec = new TrackSpecifications();
            var tracks =await unitOfWork.Repository<Track, int>().GetAllWithSpecAsync(spec);
            return tracks;
        }

        public async Task<Track?> GetTrackByIdWithSpecAsync(int id)
        {
          
            var spec = new TrackSpecifications(id);
            var track = await unitOfWork.Repository<Track, int>().GetWithSpecAsync(spec);
            return track;
        }

        public async Task<Track?> RemoveTrackAsync(int id)
        {
            var track = await GetTrackByIdWithSpecAsync(id);
            if (track is null) return null;
            unitOfWork.Repository<Track, int>().Delete(track);
            int added = await unitOfWork.CompleteAsync();
            return added > 0 ? track:null;
        }

        public async Task<Track?> UpdateTrackAsync(Track track)
        {
            unitOfWork.Repository<Track, int>().Update(track);
            int res = await unitOfWork.CompleteAsync();
            return res > 0 ? track : null;
        }
    }
}
