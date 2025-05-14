using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface ITrackService
    {
        Task<IEnumerable<Track>> GetAllTracksWithSpecAsync();

        Task<Track?> GetTrackByIdWithSpecAsync(int id);

        Task<Track?> CreateTrackAsync(Track track);
        Task<Track?> UpdateTrackAsync(Track track);
        Task<Track?> RemoveTrackAsync(int id);


    }
}
