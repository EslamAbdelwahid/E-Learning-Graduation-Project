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
        Task<IEnumerable<TrackStep>> GetAllStepsForASpecificTrackWithSpecAsync(int trackId);

        Task<TrackStep?> CreateTrackStepAsync(TrackStep trackStep);
        Task<TrackStep?> UpdateTrackStepAsync(TrackStep trackStep);
        Task<TrackStep?> DeleteTrackStepAsync(int trackId, int stepId);
        Task<TrackStep> GetStepWithSpecAsync(int trackId, int stepId);
    }
}
