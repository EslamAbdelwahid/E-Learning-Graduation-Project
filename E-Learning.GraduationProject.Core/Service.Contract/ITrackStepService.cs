using E_Learning.GraduationProject.Core.Dtos.StepResources;
using E_Learning.GraduationProject.Core.Dtos.TackSteps;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Specifications.StepResources;
using E_Learning.GraduationProject.Core.Specifications.TrackSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public interface ITrackStepService
    {
        Task<IEnumerable<TrackStep>> GetAllTrackStepsForSpecificTrackWithSpecAsync(TrackStepSpecParams specParams);
        Task<PaginationResponseToReturn<TrackStepResponseDto>> GetPaginatedTrackStepsForTrackAsync(TrackStepSpecParams specParams);

        Task<TrackStep?> CreateTrackStepAsync(TrackStep trackStep);
        Task<TrackStep?> UpdateTrackStepAsync(TrackStep trackStep);
        Task<TrackStep?> DeleteTrackStepAsync(int trackId, int stepId);
        Task<TrackStep> GetStepWithSpecAsync(int trackId, int stepId);
    }
}
