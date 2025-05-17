using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.StepResources
{
    public class StepResourceSpecifications : BaseSpecification<StepResource, int>
    {

        public StepResourceSpecifications(StepResourceSpecParams specParams) : base
        (sr => sr.TrackStepId == specParams.StepId
        && (string.IsNullOrEmpty(specParams.SearchByTitle) || sr.Title.ToLower().Contains(specParams.SearchByTitle))
        && (string.IsNullOrEmpty(specParams.SearchByResourceType) || sr.Title.ToLower().Contains(specParams.SearchByResourceType))
        ) 
        {
            
            AddOrderBy(sr => sr.OrderIndex);
            ApplyPagination(specParams.PageSize, (specParams.PageIndex - 1) * specParams.PageSize);
            ApplyIncludes();
        }
        public StepResourceSpecifications(int stepId, int resourceId) : base
        (
        sr => sr.TrackStepId == stepId 
        &&
        sr.Id == resourceId
        )
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(sr => sr.TrackStep);
        }
    }
}
