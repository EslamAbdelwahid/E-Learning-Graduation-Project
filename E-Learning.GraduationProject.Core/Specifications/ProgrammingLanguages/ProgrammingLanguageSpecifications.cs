using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.ProgrammingLanguages
{
    public class ProgrammingLanguageSpecifications : BaseSpecification<ProgrammingLanguage, int>
    {
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
