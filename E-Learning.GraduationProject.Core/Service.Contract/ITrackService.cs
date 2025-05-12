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

        Task<Track> GetTrackByIdWithSpecAsync(int id);

        Task<bool> CreateTrackAsync(Track track);
        Task<bool> UpdateTrackAsync(int id);
        Task<bool> RemoveTrackAsync(int id);


    }
}
