using E_Learning.GraduationProject.Core.Dtos.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IContactUsService
    {
        Task<ContactUsResponseDto> CreateContactAsync(ContactUsDto model);
        Task<IEnumerable<ContactUsResponseDto>?> GetAllContactsAsync();
        Task<ContactUsResponseDto?> GetContactByIdAsync(int id);
    }
}
