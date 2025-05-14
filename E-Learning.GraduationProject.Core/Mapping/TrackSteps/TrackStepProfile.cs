using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.TackSteps;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.TrackSteps
{
    public class TrackStepProfile : Profile
    {
        public TrackStepProfile()
        {
            CreateMap<CreateTrackStepDto, TrackStep>();
            CreateMap<UpdateTrackStepDto, TrackStep>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrackStepId));
            CreateMap<TrackStep, TrackStepResponseDto> ();


        }
    }
}
