using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Dtos.Contacts;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactUsResponseDto>?> GetAllContactsAsync()
        {
            var contacts = await _unitOfWork.Repository<ContactUs, int>().GetAllAsync();
            if (contacts is null) return null;

            return _mapper.Map<IEnumerable<ContactUsResponseDto>>(contacts);
        }

        public async Task<ContactUsResponseDto?> GetContactByIdAsync(int id)
        {
            var contact = await _unitOfWork.Repository<ContactUs, int>().GetByIdAsync(id);
            if (contact is null) return null;

            return _mapper.Map<ContactUsResponseDto>(contact);
        }
        public async Task<ContactUsResponseDto> CreateContactAsync(ContactUsDto model)
        {
            var entity = _mapper.Map<ContactUs>(model);

            await _unitOfWork.Repository<ContactUs, int>().AddAsync(entity);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ContactUsResponseDto>(entity); 
        }

      
    }
}
