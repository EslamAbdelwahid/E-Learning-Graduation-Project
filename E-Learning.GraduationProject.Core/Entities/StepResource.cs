using E_Learning.GraduationProject.Core.Entities.Enums;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class StepResource : BaseEntity<int>
    {
       

        public int TrackStepId { get; set; }
        public TrackStep TrackStep { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int TotalDurationMinutes { get; set; }


    }
}