using AutoMapper;
using AutoMapper.Execution;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class ProgrammingLanguageService : IProgrammingLanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProgrammingLanguageService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<IEnumerable<ProgrammingLanguageDto>?> GetAllProgrammingLanguageAsync()
        {
            var languages = await _unitOfWork.Repository<ProgrammingLanguage, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProgrammingLanguageDto>>(languages);
        }

        public async Task<ProgrammingLanguageDto?> GetProgrammingLanguageByIdAsync(int languageId)
        {
            var language = await _unitOfWork.Repository<ProgrammingLanguage, int>().GetByIdAsync(languageId);
            return _mapper.Map<ProgrammingLanguageDto>(language);
        }

        public async Task<ProgrammingLanguageDto?> CreateProgrammingLanguageAsync(ProgrammingLanguageDto model)
        {
            await _unitOfWork.Repository<ProgrammingLanguage, int>().AddAsync(_mapper.Map<ProgrammingLanguage>(model));
            var res  = await _unitOfWork.CompleteAsync();
            
            return res >0 ? model : null;
        }

        public async Task<ProgrammingLanguageDto?> UpdateProgrammingLanguageAsync(ProgrammingLanguageDto model)
        {
            _unitOfWork.Repository<ProgrammingLanguage, int>().Update(_mapper.Map<ProgrammingLanguage>(model));
            var res = await _unitOfWork.CompleteAsync();
            return res > 0 ? model : null;
        }

        public async Task<int> DeleteAsync(ProgrammingLanguageDto model)
        {
            _unitOfWork.Repository<ProgrammingLanguage, int>().Delete(_mapper.Map<ProgrammingLanguage>(model));
            return await _unitOfWork.CompleteAsync();
        }

       
    }
}
