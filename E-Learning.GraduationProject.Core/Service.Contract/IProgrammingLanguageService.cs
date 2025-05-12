using E_Learning.GraduationProject.Core.Dtos;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Specifications;
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
        Task<IEnumerable<ProgrammingLanguageDto>?> GetAllProgrammingLanguageAsync();
        Task<ProgrammingLanguageDto?> GetProgrammingLanguageByIdAsync(int languageId);
        Task<ProgrammingLanguageDto?> CreateProgrammingLanguageAsync(ProgrammingLanguageDto model);
        Task<ProgrammingLanguageDto?> UpdateProgrammingLanguageAsync(ProgrammingLanguageDto model);
        Task<int> DeleteAsync(ProgrammingLanguageDto model);


    }
}
