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

        public async Task<bool> CreateTrackAsync(Track track)
        {
            await unitOfWork.Repository<Track, int>().AddAsync(track);
            var res = await unitOfWork.CompleteAsync();
            return res > 0 ? true : false;
        }

        public Task<IEnumerable<Track>> GetAllTracksWithSpecAsync()
        {
            var spec = new TrackSpecifications();
            var tracks = unitOfWork.Repository<Track, int>().GetAllWithSpecAsync(spec);
            return tracks;
        }

        public Task<Track> GetTrackByIdWithSpecAsync(int id)
        {
          
            var spec = new TrackSpecifications(id);
            var track = unitOfWork.Repository<Track, int>().GetWithSpecAsync(spec);
            return track;
        }

        public async Task<bool> RemoveTrackAsync(int id)
        {
            var track = await GetTrackByIdWithSpecAsync(id);
            if (track is null) return false;
            unitOfWork.Repository<Track, int>().Delete(track);
            int added = await unitOfWork.CompleteAsync();
            return added > 0 ? true:false;
        }

        public Task<bool> UpdateTrackAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
