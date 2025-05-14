using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Learning.GraduationProject.Core.Dtos.StepResources;

namespace E_Learning.GraduationProject.Core.Dtos.TackSteps
{
    public class TrackStepResponseDto
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public int? EstimatedHours { get; set; }
        public bool IsRequired { get; set; }

        public ICollection<StepResourceResponseDto>? Resources { get; set; }
    }
}
