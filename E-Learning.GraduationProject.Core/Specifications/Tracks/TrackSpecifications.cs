using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Tracks
{
    public class TrackSpecifications : BaseSpecification<Track, int>
    {
        public TrackSpecifications(int trackId) : base(track => track.Id == trackId)
        {
            ApplyIncludes();
        }
        public TrackSpecifications()
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(track => track.TrackSteps);
        }
    }
}
