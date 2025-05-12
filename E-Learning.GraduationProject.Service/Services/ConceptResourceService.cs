using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.ConceptResources;
using E_Learning.GraduationProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class ConceptResourceService : IConceptResourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConceptResourceService(
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConceptResourceDto>?> GetAllResourcesAsync()
        {
            return _mapper.Map<IEnumerable<ConceptResourceDto>>(await _unitOfWork.Repository<ConceptResource, int>().GetAllAsync());
        }
        public async Task<ConceptResourceDto?> GetResourceByIdAsync(int resourceId)
        {
            return _mapper.Map<ConceptResourceDto>(await _unitOfWork.Repository<ConceptResource, int>().GetByIdAsync(resourceId));
        }

        public async Task<IEnumerable<ConceptResourceDto>?> GetAllResourcesForSpecificConceptAsync(int conceptId)
        {
            var spec = new ConceptResourceSpecifications(conceptId);
            var resources = await _unitOfWork.Repository<ConceptResource, int>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<ConceptResourceDto>>(resources);
        }

        public async Task<ConceptResourceDto?> CreateResourceAsync(ConceptResourceDto model)
        {
            await _unitOfWork.Repository<ConceptResource, int>().AddAsync(_mapper.Map<ConceptResource>(model));
            var res = await _unitOfWork.CompleteAsync();
            return res > 0 ? model : null;
        }

        public async Task<int> DeleteResourceAsync(ConceptResourceDto model)
        {
            _unitOfWork.Repository<ConceptResource, int>().Delete(_mapper.Map<ConceptResource>(model));
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<ConceptResourceDto?> UpdateResourceAsync(ConceptResourceDto model)
        {
            _unitOfWork.Repository<ConceptResource, int>().Update(_mapper.Map<ConceptResource>(model));
            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? model : null;
        }
    }
}
