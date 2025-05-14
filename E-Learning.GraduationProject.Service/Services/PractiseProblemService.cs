using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.PractiseProblems;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.PractiseProblems;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class PractiseProblemService : IPractiseProblemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PractiseProblemService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PractiseProblemToReturnDto>?> GetAllPractiseProblemAsync()
        {
            var problems = await _unitOfWork.Repository<PractiseProblem, int>().GetAllAsync();
            if (problems is null) return null;

            return _mapper.Map<IEnumerable<PractiseProblemToReturnDto>>(problems);
        }

        public async Task<PractiseProblemToReturnDto?> GetPractiseProblemByIdAsync(int id)
        {
            var spec = new PractiseProblemSpecifications(id);
            var problem = await _unitOfWork.Repository<PractiseProblem, int>().GetWithSpecAsync(spec);
            if (problem is null) return null;
            return _mapper.Map<PractiseProblemToReturnDto>(problem);
        }

        public async Task<PractiseProblemToReturnDto?> CreatePractiseProblemAsync(PractiseProblemDto model)
        {
            var entity = _mapper.Map<PractiseProblem>(model);
            await _unitOfWork.Repository<PractiseProblem, int>().AddAsync(entity);
            var result = await _unitOfWork.CompleteAsync();

            return result > 0 ? _mapper.Map<PractiseProblemToReturnDto>(entity) : null;
        }

        public async Task<int> DeletePractiseProblemAsync(int id)
        {
            var problem = await _unitOfWork.Repository<PractiseProblem, int>().GetByIdAsync(id);
            if (problem is null) return 0;
            _unitOfWork.Repository<PractiseProblem, int>().Delete(problem);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<PractiseProblemToReturnDto?> UpdatePractiseProblemAsync(PractiseProblemDto model)
        {
            var entity = _mapper.Map<PractiseProblem>(model);

            _unitOfWork.Repository<PractiseProblem, int>().Update(entity);
            var result = await _unitOfWork.CompleteAsync();

            return result > 0 ? _mapper.Map<PractiseProblemToReturnDto>(entity) : null;
        }
    }
}
