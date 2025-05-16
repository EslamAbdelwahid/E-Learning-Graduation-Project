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
        public TrackStepSpecifications()
        {
            ApplyIncludes();
        }
        public TrackStepSpecifications(int trackId) : base(ts => ts.TrackId == trackId)
        {
            ApplyIncludes();
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
