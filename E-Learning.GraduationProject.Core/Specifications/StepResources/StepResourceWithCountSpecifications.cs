using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.StepResources
{
    public class StepResourceWithCountSpecifications : BaseSpecification<StepResource, int>
    {
        public StepResourceWithCountSpecifications(StepResourceSpecParams specParams) : base
        (sr => sr.TrackStepId == specParams.StepId
        && (string.IsNullOrEmpty(specParams.SearchByTitle) || sr.Title.ToLower().Contains(specParams.SearchByTitle))
        && (string.IsNullOrEmpty(specParams.SearchByResourceType) || sr.Title.ToLower().Contains(specParams.SearchByResourceType))
        )
        {
            
        }
    }
}
