using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IStepResourceService
    {
        Task<IEnumerable<StepResource>> GetAllResourcesWithSpecAsync();
        Task<StepResource?> GetResourcesByIdWithSpecAsync(int id);
        Task<StepResource?> CreateResourceAsync(StepResource resource);
        Task<StepResource?> UpdateResourceAsync(StepResource resource);
        Task<StepResource?> DeleteResourceAsync(int id);

    }
}
