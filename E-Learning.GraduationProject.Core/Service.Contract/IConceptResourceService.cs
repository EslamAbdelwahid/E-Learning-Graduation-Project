using E_Learning.GraduationProject.Core.Dtos;
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
        Task<IEnumerable<ConceptResourceDto>?> GetAllResourcesAsync();
        Task<IEnumerable<ConceptResourceDto>?> GetAllResourcesForSpecificConceptAsync(int conceptId);
        Task<ConceptResourceDto?> GetResourceByIdAsync(int resourceId);

        Task<ConceptResourceDto?> CreateResourceAsync(ConceptResourceDto model);

        Task<ConceptResourceDto?> UpdateResourceAsync(ConceptResourceDto model);

        Task<int> DeleteResourceAsync(ConceptResourceDto model);



    }
}
