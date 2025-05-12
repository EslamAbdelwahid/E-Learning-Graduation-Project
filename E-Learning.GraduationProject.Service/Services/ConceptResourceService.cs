using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Resources;
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

        public async Task<IEnumerable<ConceptResourceToReturn>?> GetAllResourcesAsync()
        {
            return _mapper.Map<IEnumerable<ConceptResourceToReturn>>(await _unitOfWork.Repository<ConceptResource, int>().GetAllAsync());
        }
        public async Task<ConceptResourceToReturn?> GetResourceByIdAsync(int resourceId)
        {
            return _mapper.Map<ConceptResourceToReturn>(await _unitOfWork.Repository<ConceptResource, int>().GetByIdAsync(resourceId));
        }

        public async Task<IEnumerable<ConceptResourceToReturn>?> GetAllResourcesForSpecificConceptAsync(int conceptId)
        {
            var spec = new ConceptResourceSpecifications(conceptId);
            var resources = await _unitOfWork.Repository<ConceptResource, int>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<ConceptResourceToReturn>>(resources);
        }

        public async Task<ConceptResourceToReturn?> CreateResourceAsync(ConceptResourceDto model)
        {
            var entity = _mapper.Map<ConceptResource>(model);

            await _unitOfWork.Repository<ConceptResource, int>().AddAsync(entity);

            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? _mapper.Map<ConceptResourceToReturn>(entity) : null;
        }

        public async Task<int> DeleteResourceAsync(int id)
        {
            var resource = await _unitOfWork.Repository<ConceptResource, int>().GetByIdAsync(id);

            if (resource is null) return 0; 

            _unitOfWork.Repository<ConceptResource, int>().Delete(resource);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<ConceptResourceToReturn?> UpdateResourceAsync(ConceptResourceDto model)
        {
            var entity = _mapper.Map<ConceptResource>(model);

            _unitOfWork.Repository<ConceptResource, int>().Update(entity);

            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? _mapper.Map<ConceptResourceToReturn>(entity) : null;
        }
    }
}
