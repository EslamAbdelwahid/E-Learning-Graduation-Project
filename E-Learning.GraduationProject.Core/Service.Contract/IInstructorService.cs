using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Specifications.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IInstructorService
    {
        public Task<PaginationResponseToReturn<InstructorToReturnDto>?> GetAllInstructorsAsync(InstructorParams specParams);
        public Task<InstructorToReturnDto?> GetInstructorByIdAsync(int id);
        public Task<InstructorToReturnDto?> CreateInstructorAsync(InstructorDto model);
        public Task<InstructorToReturnDto?> UpdateInstructorAsync(int id , InstructorDto model);
        public Task<int> DeleteInstructorAsync(int id);
        


        
    }
}
