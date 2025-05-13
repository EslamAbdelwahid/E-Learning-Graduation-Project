using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.LanguageConcepts
{
    public class LanguageConceptSpecifications : BaseSpecification<LanguageConcept,int>
    {
        public LanguageConceptSpecifications(int id) : base(
            LC => LC.Id == id
            )
        {
            ApplyIncludes();
        }
        public LanguageConceptSpecifications()
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(LC => LC.ProgrammingLanguage);
            Includes.Add(LC => LC.Resources);
            Includes.Add(LC => LC.Problems);
        }
        
    }
}
