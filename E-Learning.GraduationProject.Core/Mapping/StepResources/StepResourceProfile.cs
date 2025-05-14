using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.StepResources;
using E_Learning.GraduationProject.Core.Dtos.TackSteps;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.StepResources
{
    public class StepResourceProfile : Profile
    {
        public StepResourceProfile()
        {
            CreateMap<CreateStepResourceDto, StepResource>();
            CreateMap<UpdateStepResourceDto, StepResource>();
            CreateMap<StepResource, StepResourceResponseDto>();
        }
    }
}
