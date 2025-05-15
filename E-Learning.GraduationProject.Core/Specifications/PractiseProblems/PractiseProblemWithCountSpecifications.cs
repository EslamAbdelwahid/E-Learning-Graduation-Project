using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.PractiseProblems
{
    public class PractiseProblemWithCountSpecifications :BaseSpecification<PractiseProblem,int>
    {
        public PractiseProblemWithCountSpecifications(ParctiseProblemParames parames) : base(
             PP =>
            (string.IsNullOrEmpty(parames.Search) || PP.Title.ToLower().Contains(parames.Search))
            &&
            (string.IsNullOrEmpty(parames.ProblemDifficultyLevel) || PP.ProblemDifficultyLevel == Enum.Parse<ProblemDifficultyLevel>( parames.ProblemDifficultyLevel))
            &&
            (string.IsNullOrEmpty(parames.PlatformName) || PP.PlatformName == Enum.Parse<PlatformName>(parames.PlatformName))
            &&
            (string.IsNullOrEmpty(parames.EntityType) || PP.EntityType == Enum.Parse<EntityType>(parames.EntityType))
            )
        {
            
        }
    }
}
