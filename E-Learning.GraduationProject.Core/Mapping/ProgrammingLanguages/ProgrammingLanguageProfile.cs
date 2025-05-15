using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.ProgrammingLanguages
{
    public class ProgrammingLanguageProfile : Profile
    {
        public ProgrammingLanguageProfile()
        {
            CreateMap<ProgrammingLanguageDto, ProgrammingLanguage>();
            CreateMap<ProgrammingLanguage, ProgrammingLanguageToReturnDto>();
        }
    }
}
