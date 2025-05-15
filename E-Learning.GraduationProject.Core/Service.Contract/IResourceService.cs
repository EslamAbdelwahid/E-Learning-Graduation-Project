using E_Learning.GraduationProject.Core.Dtos.Resources;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Specifications.ConceptResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IResourceService
    {
        // pagination ToDo
        Task<PaginationResponseToReturn<ConceptResourceToReturn>?> GetAllResourcesAsync(ConceptResourceParames parames);
        Task<IEnumerable<ConceptResourceToReturn>?> GetAllResourcesForSpecificLanguageAsync(int languageId);
        Task<ConceptResourceToReturn?> GetResourceByIdAsync(int resourceId);

        Task<ConceptResourceToReturn?> CreateResourceAsync(ConceptResourceDto model);

        Task<ConceptResourceToReturn?> UpdateResourceAsync(ConceptResourceDto model);

        Task<int> DeleteResourceAsync(int id);



    }
}
