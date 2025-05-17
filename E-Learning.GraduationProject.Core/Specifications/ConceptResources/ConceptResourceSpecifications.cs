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
        public ConceptResourceSpecifications(ConceptResourceParames parames): base(
            CR =>
            (string.IsNullOrEmpty(parames.Search) ||  CR.Title.ToLower().Contains( parames.Search))
            )
        {

            if(! string.IsNullOrEmpty(parames.Sort))
            {
                switch (parames.Sort)
                {
                    case "Duration_Asc":
                        AddOrderBy(CR => CR.TotalDurationMinutes);
                        break;
                    case "Duration_Desc":
                        AddOrderByDesc(CR => CR.TotalDurationMinutes);
                        break;
                    case "Title_Asc":
                        AddOrderBy(CR => CR.Title);
                        break;
                    case "Title_Desc":
                        AddOrderByDesc(CR => CR.Title);
                        break;
                    default:
                        AddOrderBy(CR => CR.Title);
                        break;

                }
            }
            else // sort by title 
            {
                AddOrderBy(CR => CR.Title);
            }

            // apply pagination [take , leave ]
            ApplyPagination(parames.PageSize, parames.PageSize * (parames.PageIndex - 1));  // Size = 5 , index = 3

            ApplyIncludes(); 
        }
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
