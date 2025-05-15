using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.ProgrammingLanguages
{
    public class ProgrammingLanguageWithCountSpecifications : BaseSpecification<ProgrammingLanguage , int >
    {
        public ProgrammingLanguageWithCountSpecifications(ProgrammingLanguageParames parames) : base(
             PL =>
            (string.IsNullOrEmpty(parames.Search) || PL.Name.ToLower().Contains(parames.Search))
            &&
            (string.IsNullOrEmpty(parames.DifficultyLevel) || PL.Difficulty == Enum.Parse<DifficultyLevel>(parames.DifficultyLevel))

            )
        {
            
        }

    }
}
