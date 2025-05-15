using AutoMapper;
using AutoMapper.Execution;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.ProgrammingLanguages;
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



        public async Task<PaginationResponseToReturn<ProgrammingLanguageToReturnDto>?> GetAllProgrammingLanguageWithSpecAsync(ProgrammingLanguageParames parames )
        {
            var spec = new ProgrammingLanguageSpecifications(parames);

            var languages = await _unitOfWork.Repository<ProgrammingLanguage, int>().GetAllWithSpecAsync(spec);

            if (languages is null) return null;

             var data = _mapper.Map<IEnumerable<ProgrammingLanguageToReturnDto>>(languages);

            var countSpec = new ProgrammingLanguageWithCountSpecifications(parames);

            var count = await _unitOfWork.Repository<ProgrammingLanguage, int>().GetCountAsync(countSpec);

            return new PaginationResponseToReturn<ProgrammingLanguageToReturnDto>(parames.PageIndex, parames.PageSize, count, data);

        }

        public async Task<ProgrammingLanguageToReturnDto?> GetProgrammingLanguageByIdWithSpecAsync(int languageId)
        {
            var spec = new ProgrammingLanguageSpecifications(languageId);

            var language = await _unitOfWork.Repository<ProgrammingLanguage, int>().GetWithSpecAsync(spec);

            if (language is null) return null;

            return _mapper.Map<ProgrammingLanguageToReturnDto>(language);
        }

        public async Task<ProgrammingLanguageToReturnDto?> CreateProgrammingLanguageAsync(ProgrammingLanguageDto model)
        {
            var entity = _mapper.Map<ProgrammingLanguage>(model);

            await _unitOfWork.Repository<ProgrammingLanguage, int>().AddAsync(entity);

            var res  = await _unitOfWork.CompleteAsync();

            // this remap add any database generated values (createdAt or PK id) to the Dto 
            return res > 0 ? _mapper.Map<ProgrammingLanguageToReturnDto>(entity) : null;
        }

        public async Task<ProgrammingLanguageToReturnDto?> UpdateProgrammingLanguageAsync(ProgrammingLanguageDto model)
        {
            var entity = _mapper.Map<ProgrammingLanguage>(model);

            _unitOfWork.Repository<ProgrammingLanguage, int>().Update(entity);

            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? _mapper.Map<ProgrammingLanguageToReturnDto>(entity) : null;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<ProgrammingLanguage, int>().GetByIdAsync(id);

            if (entity is null) return 0;

            _unitOfWork.Repository<ProgrammingLanguage, int>().Delete(entity);

            return await _unitOfWork.CompleteAsync();
        }

       
    }
}
