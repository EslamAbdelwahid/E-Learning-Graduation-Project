using E_Learning.GraduationProject.Core.Entities.Enums;
using System.Text.Json.Serialization;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class ConceptResource : BaseEntity<int>
    {
        [JsonPropertyName("ResourceId")]
        public override int Id { get; set; }
        public string Title { get; set; }

        public int? LanguageConceptId { get; set; }
        public LanguageConcept? LanguageConcept { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }
        public int TotalDurationMinutes { get; set; }

    }
}