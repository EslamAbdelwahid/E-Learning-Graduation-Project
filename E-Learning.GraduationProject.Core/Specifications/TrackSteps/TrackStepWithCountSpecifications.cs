using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.TrackSteps
{
    public class TrackStepWithCountSpecifications : BaseSpecification<TrackStep, int>
    {
        public TrackStepWithCountSpecifications(TrackStepSpecParams specParams) : base
            (
            ts => ts.TrackId == specParams.TrackId
            &&
            (string.IsNullOrEmpty(specParams.SearchByTitle) || (ts.Title.ToLower().Contains(specParams.SearchByTitle)))
            )
        {
            
        }
    }
}
