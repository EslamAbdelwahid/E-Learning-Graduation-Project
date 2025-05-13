using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.LanguageConcepts;
using E_Learning.GraduationProject.Core.Dtos.Resources;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.LanguageConcepts
{
    public class LanguageConceptProfile :Profile
    {
        public LanguageConceptProfile()
        {
            CreateMap<LanguageConcept, LanguageConceptToReturnDto>();
            CreateMap<LanguageConceptDto, LanguageConcept>();

        }
    }
}
