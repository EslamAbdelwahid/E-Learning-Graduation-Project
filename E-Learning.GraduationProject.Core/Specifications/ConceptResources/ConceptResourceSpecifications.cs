using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.ConceptResources
{
    public class ConceptResourceSpecifications : BaseSpecification<ConceptResource,int>
    {

        public ConceptResourceSpecifications(int conceptId) : base(
                CR => CR.LanguageConceptId == conceptId 
            )
        {
            ApplyIncludes();
        }

        public ConceptResourceSpecifications()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(CR => CR.LanguageConcept);
        }
    }
}
