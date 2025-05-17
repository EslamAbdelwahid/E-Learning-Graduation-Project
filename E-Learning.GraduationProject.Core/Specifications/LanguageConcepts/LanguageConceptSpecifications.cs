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
                    case "Title_Asc":
                        AddOrderBy(LC => LC.Title);
                        break;
                    case "Title_Desc":
                        AddOrderByDesc(LC => LC.Title);
                        break;
                    case "Estimated_Hours_Asc":
                        AddOrderBy(LC => LC.EstimatedHours);
                        break;
                    case "Estimated_Hours_Desc":
                        AddOrderByDesc(LC => LC.EstimatedHours);
                        break;
                    case "Order_Index_Asc":
                        AddOrderBy(LC => LC.OrderIndex);
                        break;
                    case "Order_Index_Desc": 
                        AddOrderByDesc(LC => LC.OrderIndex);
                        break;
                    default:
                        AddOrderBy(LC => LC.OrderIndex);
                        break;
                }
               
            }
            else // OrderIndex 
            {
                AddOrderBy(LC => LC.OrderIndex);
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
