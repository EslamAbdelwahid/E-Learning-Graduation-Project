using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.PractiseProblems;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Mapping.PractiseProblems
{
    public class PractiseProbemProfile : Profile
    {
        public PractiseProbemProfile()
        {
            CreateMap<PractiseProblem, PractiseProblemToReturnDto>();
            CreateMap<PractiseProblemDto, PractiseProblem>();
        }
    }
}
