using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class Track:BaseEntity<int>
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? EstimatedCompletionWeeks { get; set; }
        public bool IsActive { get; set; }
        public ICollection<TrackStep>? TrackSteps { get; set; }

    }
}
