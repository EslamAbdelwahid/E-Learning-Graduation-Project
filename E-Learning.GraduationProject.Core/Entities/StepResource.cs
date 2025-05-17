using E_Learning.GraduationProject.Core.Entities.Enums;
using System.Text.Json.Serialization;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class StepResource : BaseEntity<int>
    {
        [JsonPropertyName("StepResourceId")]
        public override int Id { get; set; }
        public int TrackStepId { get; set; }
        public TrackStep TrackStep { get; set; }

        public int OrderIndex { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int TotalDurationMinutes { get; set; }


    }
}