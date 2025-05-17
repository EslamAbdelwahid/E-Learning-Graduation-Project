using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.StepResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class StepResourceService : IStepResourceService
    {
        private readonly IUnitOfWork unitOfWork;

        public StepResourceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<StepResource?> CreateResourceAsync(StepResource resource)
        {
            await unitOfWork.Repository<StepResource, int>().AddAsync(resource);
            int res = await unitOfWork.CompleteAsync();
            return res > 0 ? resource : null;
        }
        public async Task<StepResource?> UpdateResourceAsync(StepResource resource)
        {
            unitOfWork.Repository<StepResource, int>().Update(resource);
            int res = await unitOfWork.CompleteAsync();
            return res > 0 ? resource : null;
        }
        public async Task<StepResource?> DeleteResourceAsync(int stepId, int resourceId)
        {
            var resource = await GetResourcesWithSpecAsync(stepId, resourceId);
            if (resource is null) return null;
            unitOfWork.Repository<StepResource, int>().Delete(resource);
            int res = await unitOfWork.CompleteAsync();
            return res > 0 ? resource : null;
        }

        public async Task<IEnumerable<StepResource>> GetAllResourcesForSpecificStepWithSpecAsync(StepResourceSpecParams specParams)
        {
            var spec = new StepResourceSpecifications(specParams);
            var resources = await unitOfWork.Repository<StepResource, int>().GetAllWithSpecAsync(spec);
            return resources;
        }

        public async Task<StepResource?> GetResourcesWithSpecAsync(int stepId, int resourceId)
        {
            var spec = new StepResourceSpecifications(stepId, resourceId);
            var resource = await unitOfWork.Repository<StepResource, int>().GetWithSpecAsync(spec);
            return resource;
        }


    }
}
