using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Tracks
{
    public class CreateTrackDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? EstimatedCompletionWeeks { get; set; }
    }
}
