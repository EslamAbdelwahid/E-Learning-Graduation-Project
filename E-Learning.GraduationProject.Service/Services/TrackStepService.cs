using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.TrackSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class TrackStepService : ITrackStepService
    {
        private readonly IUnitOfWork unitOfWork;

        public TrackStepService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<TrackStep?> CreateTrackStepAsync(TrackStep trackStep)
        {
            await unitOfWork.Repository<TrackStep, int>().AddAsync(trackStep);
            int added = await unitOfWork.CompleteAsync();
            return (added > 0 ? trackStep : null);
        }

        public async Task<TrackStep?> DeleteTrackStepAsync(int id)
        {
            var track = await GetStepByIdWithSpecAsync(id);
            unitOfWork.Repository<TrackStep, int>().Delete(track);
            int res = await unitOfWork.CompleteAsync();
            return res > 0 ? track : null;
        }

        public async Task<IEnumerable<TrackStep>> GetAllStepsWithSpecAsync()
        {
            var spec = new TrackStepSpecifications();
            var trackSteps = await unitOfWork.Repository<TrackStep, int>().GetAllWithSpecAsync(spec);
            return trackSteps;
        }

        public async Task<TrackStep> GetStepByIdWithSpecAsync(int id)
        {
            var spec = new TrackStepSpecifications(id);
            var trackStep = await unitOfWork.Repository<TrackStep, int>().GetWithSpecAsync(spec);
            return trackStep;
        }

        public async Task<TrackStep?> UpdateTrackStepAsync(TrackStep trackStep)
        {
            unitOfWork.Repository<TrackStep, int>().Update(trackStep);
            int res = await unitOfWork.CompleteAsync();
            return res > 0 ? trackStep : null;
        }
    }
}
