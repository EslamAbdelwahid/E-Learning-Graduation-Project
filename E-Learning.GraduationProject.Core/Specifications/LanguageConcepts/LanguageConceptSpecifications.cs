using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.LanguageConcepts
{
    public class LanguageConceptSpecifications : BaseSpecification<LanguageConcept,int>
    {
        public LanguageConceptSpecifications(LanguageConceptParames parames) : base (
            LC=>
            (string.IsNullOrEmpty(parames.Search) || LC.Title.ToLower().Contains(parames.Search))
            )
        {
            if(! string.IsNullOrEmpty( parames.Sort))
            {
                switch (parames.Sort)
                {
                    case "TitleAscending":
                        AddOrderBy(LC => LC.Title);
                        break;
                    case "TitleDescending":
                        AddOrderByDesc(LC => LC.Title);
                        break;
                    case "EstimatedHoursAscending":
                        AddOrderBy(LC => LC.EstimatedHours);
                        break;
                    case "EstimatedHoursDescending":
                        AddOrderByDesc(LC => LC.EstimatedHours);
                        break;
                    case "OrderIndexAscending":
                        AddOrderBy(LC => LC.OrderIndex);
                        break;
                    case "OrderIndexDescending":
                        AddOrderByDesc(LC => LC.OrderIndex);
                        break;
                    default:
                        AddOrderBy(LC => LC.Title);
                        break;
                }
               
            }
            else // title
            {
                AddOrderBy(LC => LC.Title);
            }

            // pagination
            ApplyPagination(parames.PageSize, parames.PageSize * (parames.PageIndex - 1));

            ApplyIncludes();


        }

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
