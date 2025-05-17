using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.ProgrammingLanguages
{
    public class ProgrammingLanguageSpecifications : BaseSpecification<ProgrammingLanguage, int>
    {

        public ProgrammingLanguageSpecifications(ProgrammingLanguageParames parames) : base(
            PL => 
            (string.IsNullOrEmpty(parames.Search) || PL.Name.ToLower().Contains(parames.Search))
            &&
            (string.IsNullOrEmpty(parames.DifficultyLevel) || PL.Difficulty == Enum.Parse<DifficultyLevel>(parames.DifficultyLevel))
            )
        {
            if(!string.IsNullOrEmpty(parames.Sort))
            {
                switch (parames.Sort)
                {
                    case "Name_Asc":
                        AddOrderBy(PL => PL.Name);
                        break;
                    case "Name_Desc":
                        AddOrderByDesc(PL => PL.Name);
                        break;
                    default:
                        AddOrderBy(PL => PL.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(PL => PL.Name);
            }

            ApplyPagination(parames.PageSize, parames.PageSize * (parames.PageIndex - 1));
            ApplyIncludes();

        }

        public ProgrammingLanguageSpecifications(int id ) : base(
            PL => PL.Id == id 
            )
        {
            ApplyIncludes();
        }

        public ProgrammingLanguageSpecifications()
        {
            ApplyIncludes();
        }



        private void ApplyIncludes()
        {
            Includes.Add(PL => PL.Concepts);
        }


    }
}
