using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstructorService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponseToReturn<InstructorToReturnDto>?> GetAllInstructorsAsync(InstructorParams specParams)
        {
            var spec = new InstructorSpecifications(specParams);

            var instructors = await _unitOfWork.Repository<Instructor, int>().GetAllWithSpecAsync(spec);
            if (instructors is null) return null;
            var data = _mapper.Map<IEnumerable<InstructorToReturnDto>>(instructors);

            var countSpec = new InstructorWithCountSpecifications(specParams);

            var count = await _unitOfWork.Repository<Instructor, int>().GetCountAsync(countSpec);

            return new PaginationResponseToReturn<InstructorToReturnDto>(specParams.PageIndex, specParams.PageSize, count, data);
        }

        public async Task<InstructorToReturnDto?> GetInstructorByIdAsync(int id)
        {
            var spec = new InstructorSpecifications(id);
            var instructor = await _unitOfWork.Repository<Instructor, int>().GetWithSpecAsync(spec);
            if (instructor is null) return null; 

            return _mapper.Map<InstructorToReturnDto>(instructor);

        }
        public async Task<InstructorToReturnDto?> CreateInstructorAsync(InstructorDto model)
        {
            var instructor = _mapper.Map<Instructor>(model);

            await _unitOfWork.Repository<Instructor, int>().AddAsync(instructor);

            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? _mapper.Map<InstructorToReturnDto>(instructor) : null;
        }
        public async Task<InstructorToReturnDto?> UpdateInstructorAsync(int id , InstructorDto model)
        {
            var existanceInstructor = await _unitOfWork.Repository<Instructor, int>().GetByIdAsync(id);
            if (existanceInstructor == null) return null;

            var instructor = _mapper.Map(model, existanceInstructor);

            _unitOfWork.Repository<Instructor, int>().Update(instructor);
            var res = await _unitOfWork.CompleteAsync();

            return res > 0 ? _mapper.Map<InstructorToReturnDto>(instructor) : null;
        }

        public async Task<int> DeleteInstructorAsync(int id)
        {
            var entity = await _unitOfWork.Repository<Instructor, int>().GetByIdAsync(id);

            if (entity is null) return 0;

            _unitOfWork.Repository<Instructor, int>().Delete(entity);

            return await _unitOfWork.CompleteAsync();
            
        }

    }
}
