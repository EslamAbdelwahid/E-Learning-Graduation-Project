using E_Learning.GraduationProject.Core.Dtos.StepResources;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Specifications.StepResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IStepResourceService
    {
        Task<IEnumerable<StepResource>> GetAllResourcesForSpecificStepWithSpecAsync(StepResourceSpecParams specParams);
        Task<PaginationResponseToReturn<StepResourceResponseDto>> GetPaginatedResourcesForStepAsync(StepResourceSpecParams specParams);
        Task<StepResource?> GetResourcesWithSpecAsync(int stepId, int resourceId);
        Task<StepResource?> CreateResourceAsync(StepResource resource);
        Task<StepResource?> UpdateResourceAsync(StepResource resource);
        Task<StepResource?> DeleteResourceAsync(int stepId, int resourceId);

    }
}
