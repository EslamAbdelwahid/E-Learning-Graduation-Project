using E_Learning.GraduationProject.Core.Dtos.PractiseProblems;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Specifications.PractiseProblems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IPractiseProblemService
    {
        Task<PaginationResponseToReturn<PractiseProblemToReturnDto>?> GetAllPractiseProblemAsync(ParctiseProblemParames parames);
        Task<PractiseProblemToReturnDto?> GetPractiseProblemByIdAsync(int id );
        Task<PractiseProblemToReturnDto?> CreatePractiseProblemAsync(PractiseProblemDto model);
        Task<PractiseProblemToReturnDto?> UpdatePractiseProblemAsync(PractiseProblemDto model);
        Task<int> DeletePractiseProblemAsync(int id);

    }
}
