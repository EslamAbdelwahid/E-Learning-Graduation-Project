using E_Learning.GraduationProject.Core.Dtos.LanguageConcepts;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IConceptService
    {
        Task<IEnumerable< LanguageConceptToReturnDto>?> GetAllConceptsWithSpecAsync();
        Task<LanguageConceptToReturnDto?> GetConceptByIdWithSpec(int id);
        Task<LanguageConceptToReturnDto?> CreateConcept(LanguageConceptDto model);
        Task<LanguageConceptToReturnDto?> UpdateConcept(LanguageConceptDto model);
        Task<int> DeleteConcept(int id);

    }
}
