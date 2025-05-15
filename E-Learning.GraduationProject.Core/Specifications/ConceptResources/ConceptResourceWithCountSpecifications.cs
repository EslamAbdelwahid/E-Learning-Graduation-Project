using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.ConceptResources
{
    public class ConceptResourceWithCountSpecifications : BaseSpecification<ConceptResource,int>
    {
        public ConceptResourceWithCountSpecifications(ConceptResourceParames parames ) : base(
            P =>

            (string.IsNullOrEmpty( parames.Search ) || P.Title.ToLower().Contains(parames.Search))
            
            )
        {
            // no need for applying Includes here u are using this for only counting not retrieving data
        }

    
    }
}
