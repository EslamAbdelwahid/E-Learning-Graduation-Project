using E_Learning.GraduationProject.Core.Dtos.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Specifications;
using E_Learning.GraduationProject.Core.Specifications.ProgrammingLanguages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IProgrammingLanguageService
    {
        // pagination to do 
        Task<PaginationResponseToReturn<ProgrammingLanguageToReturnDto>?> GetAllProgrammingLanguageWithSpecAsync(ProgrammingLanguageParames parames );
        Task<ProgrammingLanguageToReturnDto?> GetProgrammingLanguageByIdWithSpecAsync(int languageId);
        Task<ProgrammingLanguageToReturnDto?> CreateProgrammingLanguageAsync(ProgrammingLanguageDto model);
        Task<ProgrammingLanguageToReturnDto?> UpdateProgrammingLanguageAsync(ProgrammingLanguageDto model);
        Task<int> DeleteAsync(int id);


    }
}
