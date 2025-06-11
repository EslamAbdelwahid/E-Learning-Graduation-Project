using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Adresses;
using E_Learning.GraduationProject.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Addresses
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressToReturnDto>().ReverseMap();
        }
    }
}
