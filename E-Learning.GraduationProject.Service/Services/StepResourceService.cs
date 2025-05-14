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
        public async Task<StepResource?> DeleteResourceAsync(int id)
        {
            var resource = await GetResourcesByIdWithSpecAsync(id);
            if (resource is null) return null;
            unitOfWork.Repository<StepResource, int>().Delete(resource);
            int res = await unitOfWork.CompleteAsync();
            return res > 0 ? resource : null;
        }

        public async Task<IEnumerable<StepResource>> GetAllResourcesWithSpecAsync()
        {
            var spec = new StepResourceSpecifications();
            var resources = await unitOfWork.Repository<StepResource, int>().GetAllWithSpecAsync(spec);
            return resources;
        }

        public async Task<StepResource?> GetResourcesByIdWithSpecAsync(int id)
        {
            var spec = new StepResourceSpecifications(id);
            var resource = await unitOfWork.Repository<StepResource, int>().GetWithSpecAsync(spec);
            return resource;
        }


    }
}
