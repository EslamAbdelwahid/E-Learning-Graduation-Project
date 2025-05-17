using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.TrackSteps
{
    public class TrackStepSpecifications : BaseSpecification<TrackStep, int>
    {

        public TrackStepSpecifications(TrackStepSpecParams specParams) : base
            (
            ts => ts.TrackId == specParams.TrackId
            &&
            (string.IsNullOrEmpty(specParams.SearchByTitle) || (ts.Title.ToLower().Contains(specParams.SearchByTitle)))
            )
        {
            

            ApplyIncludes();
            ApplyPagination(specParams.PageSize, (specParams.PageIndex - 1) * specParams.PageSize);
            AddOrderBy(ts => ts.OrderIndex);
            
        }
        public TrackStepSpecifications(int trackId, int stepId) : base(ts => ts.TrackId == trackId 
        && ts.Id == stepId)
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(ts => ts.Resources);
        }
    }
}
