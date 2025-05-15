using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.StepResources
{
    public class CreateStepResourceDto
    {
        public int TrackStepId {  get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public ResourceType ResourceType { get; set; }
        public int TotalDurationMinutes { get; set; }
    }
}
