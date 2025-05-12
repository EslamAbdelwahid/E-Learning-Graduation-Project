using E_Learning.GraduationProject.Core.Dtos.Resources;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IConceptResourceService
    {
        // pagination ToDo
        Task<IEnumerable<ConceptResourceToReturn>?> GetAllResourcesAsync();
        Task<IEnumerable<ConceptResourceToReturn>?> GetAllResourcesForSpecificConceptAsync(int conceptId);
        Task<ConceptResourceToReturn?> GetResourceByIdAsync(int resourceId);

        Task<ConceptResourceToReturn?> CreateResourceAsync(ConceptResourceDto model);

        Task<ConceptResourceToReturn?> UpdateResourceAsync(ConceptResourceDto model);

        Task<int> DeleteResourceAsync(int id);



    }
}
