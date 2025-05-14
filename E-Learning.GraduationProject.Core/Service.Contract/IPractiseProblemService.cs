using E_Learning.GraduationProject.Core.Dtos.PractiseProblems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IPractiseProblemService
    {
        Task<IEnumerable<PractiseProblemToReturnDto>?> GetAllPractiseProblemAsync();
        Task<PractiseProblemToReturnDto?> GetPractiseProblemByIdAsync(int id );
        Task<PractiseProblemToReturnDto?> CreatePractiseProblemAsync(PractiseProblemDto model);
        Task<PractiseProblemToReturnDto?> UpdatePractiseProblemAsync(PractiseProblemDto model);
        Task<int> DeletePractiseProblemAsync(int id);

    }
}
