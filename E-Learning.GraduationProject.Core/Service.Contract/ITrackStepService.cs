using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public interface ITrackStepService
    {
        Task<IEnumerable<TrackStep>> GetAllStepsWithSpecAsync();
        Task<TrackStep> GetStepByIdWithSpecAsync(int id);

        Task<TrackStep?> CreateTrackStepAsync(TrackStep trackStep);
        Task<TrackStep?> UpdateTrackStepAsync(TrackStep trackStep);
        Task<TrackStep?> DeleteTrackStepAsync(int id);

    }
}
