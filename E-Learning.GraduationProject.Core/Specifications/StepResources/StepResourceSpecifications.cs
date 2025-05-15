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
        public StepResourceSpecifications()
        {
            ApplyIncludes();
        }
        public StepResourceSpecifications(int resourceId):base(sr => sr.Id == resourceId) 
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(sr => sr.TrackStep);
        }
    }
}
