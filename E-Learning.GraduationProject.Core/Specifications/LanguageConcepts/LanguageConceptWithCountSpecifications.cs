using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.LanguageConcepts
{
    public class LanguageConceptWithCountSpecifications : BaseSpecification<LanguageConcept,int>
    {
        public LanguageConceptWithCountSpecifications(LanguageConceptParames parames) : base(
            P =>
            (string.IsNullOrEmpty(parames.Search) || P.Title.ToLower().Contains(parames.Search))
            )
        {
           
        }

        

    }
}
