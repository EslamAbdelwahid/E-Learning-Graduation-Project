using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Contacts;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Contacts
{
    public class ContactUsProfile : Profile
    {
        public ContactUsProfile()
        {
            CreateMap<ContactUs, ContactUsResponseDto>();
            CreateMap<ContactUsDto, ContactUs>();

        }
    }
}
