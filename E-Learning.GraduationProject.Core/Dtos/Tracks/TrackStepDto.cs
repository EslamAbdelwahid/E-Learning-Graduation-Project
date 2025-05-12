using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Tracks
{
    public class TrackStepDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public int? EstimatedHours { get; set; }
        public bool IsRequired { get; set; }
    }
}
