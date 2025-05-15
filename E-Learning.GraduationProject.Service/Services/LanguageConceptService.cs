using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.LanguageConcepts;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.LanguageConcepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class LanguageConceptService : ILanguageConceptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LanguageConceptService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponseToReturn<LanguageConceptToReturnDto>?> GetAllConceptsWithSpecAsync(LanguageConceptParames parames)
        {
            var spec = new LanguageConceptSpecifications(parames);

            var concepts = await _unitOfWork.Repository<LanguageConcept, int>().GetAllWithSpecAsync(spec);

            if (concepts is null) return null;

            var data = _mapper.Map<IEnumerable<LanguageConceptToReturnDto>>(concepts);

            var countSpec = new LanguageConceptWithCountSpecifications(parames);

            var count = await _unitOfWork.Repository<LanguageConcept, int>().GetCountAsync(countSpec);

            return new PaginationResponseToReturn<LanguageConceptToReturnDto>(parames.PageIndex,parames.PageSize,count,data);
        }

        public async Task<LanguageConceptToReturnDto?> GetConceptByIdWithSpec(int id)
        {
            var spec = new LanguageConceptSpecifications(id);

            var concept = await  _unitOfWork.Repository<LanguageConcept, int>().GetWithSpecAsync(spec);

            if (concept is null) return null;

            return _mapper.Map<LanguageConceptToReturnDto>(concept);
        }

        public async Task<LanguageConceptToReturnDto?> CreateConcept(LanguageConceptDto model)
        {
            var entity = _mapper.Map<LanguageConcept>(model);

            await _unitOfWork.Repository<LanguageConcept, int>().AddAsync(entity);

            var result = await _unitOfWork.CompleteAsync();

            return result > 0 ? _mapper.Map<LanguageConceptToReturnDto>(entity) : null;
        }

        public async Task<int> DeleteConcept(int id)
        {
            var entity = await _unitOfWork.Repository<LanguageConcept, int>().GetByIdAsync(id);
            if (entity is null) return 0;

             _unitOfWork.Repository<LanguageConcept, int>().Delete(entity);

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<LanguageConceptToReturnDto?> UpdateConcept(LanguageConceptDto model)
        {
            var entity = _mapper.Map<LanguageConcept>(model);

            _unitOfWork.Repository<LanguageConcept, int>().Update(entity);

            var result =  await _unitOfWork.CompleteAsync();

            return result > 0 ? _mapper.Map<LanguageConceptToReturnDto>(entity) : null;
        }
    }
}
