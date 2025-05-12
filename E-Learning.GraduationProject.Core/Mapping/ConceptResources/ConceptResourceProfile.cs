using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Resources;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.ConceptResources
{
    public class ConceptResourceProfile : Profile
    {
        public ConceptResourceProfile()
        {
            CreateMap<ConceptResource, ConceptResourceDto>()
                .ForMember(D => D.ConceptName, options => options.MapFrom(S => S.LanguageConcept.Title));//.ReverseMap();

            CreateMap<ConceptResourceDto, ConceptResource>()
                .ForMember(dest => dest.LanguageConcept, opt => opt.Ignore());

            CreateMap<ConceptResource, ConceptResourceToReturn>();
        }
    }
}
