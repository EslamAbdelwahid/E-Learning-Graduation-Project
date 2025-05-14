using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Tracks;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.Tracks
{
    public class TrackProfile : Profile
    {
        public TrackProfile()
        {
            CreateMap<Track, TrackResponseDto>();
            CreateMap<CreateTrackDto, Track>();
            CreateMap<UpdateTrackDto, Track>();
        }
    }
}
