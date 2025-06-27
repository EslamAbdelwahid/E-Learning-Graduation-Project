using E_Learning.GraduationProject.Core.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class Track : BaseEntity<int>
    {
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }


        public string IconUrl { get; set; }

        [JsonPropertyName("estimatedCompletionWeeks")]
        public int? EstimatedCompletionWeeks { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        public ICollection<TrackStep>? TrackSteps { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
