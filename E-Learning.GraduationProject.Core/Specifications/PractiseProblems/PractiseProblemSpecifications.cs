using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.PractiseProblems
{
    public class PractiseProblemSpecifications : BaseSpecification<PractiseProblem, int>
    {
        public PractiseProblemSpecifications(ParctiseProblemParames parames) : base(
            PP =>
            (string.IsNullOrEmpty(parames.Search) || PP.Title.ToLower().Contains(parames.Search))
            &&
            (string.IsNullOrEmpty(parames.ProblemDifficultyLevel) || PP.ProblemDifficultyLevel == Enum.Parse<ProblemDifficultyLevel>( parames.ProblemDifficultyLevel))
            &&
            (string.IsNullOrEmpty(parames.PlatformName) || PP.PlatformName == Enum.Parse<PlatformName>( parames.PlatformName))
            &&
            (string.IsNullOrEmpty(parames.EntityType) || PP.EntityType == Enum.Parse<EntityType>( parames.EntityType))

            )
        {
            if (!string.IsNullOrEmpty(parames.Sort))
            {
                switch (parames.Sort)
                {
                    case "TitleAscending":
                        AddOrderBy(PP => PP.Title);
                        break;
                    case "TitleDescending":
                        AddOrderByDesc(PP => PP.Title);
                        break;
                    case "OrderIndexAscending":
                        AddOrderBy(PP => PP.OrderIndex);
                        break;
                    case "OrderIndexDescending":
                        AddOrderByDesc(PP => PP.OrderIndex);
                        break;

                    default:
                        AddOrderBy(PP => PP.Title);
                        break;
                }
                
               
            }
            else
            {
                AddOrderBy(PP => PP.Title);
            }
        }


        public PractiseProblemSpecifications(int id) : base(
            PP => PP.Id == id
            )
        {

        }
        public PractiseProblemSpecifications()
        {

        }


    }
}
