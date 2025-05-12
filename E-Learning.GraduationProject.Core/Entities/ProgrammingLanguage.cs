using E_Learning.GraduationProject.Core.Entities.Enums;
using System.Text.Json.Serialization;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class ProgrammingLanguage : BaseEntity<int>
    {
        [JsonPropertyName("languageId")]
        public override int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string IconUrl { get; set; }
        [JsonPropertyName("DifficultyLevel")]
        public DifficultyLevel Difficulty { get; set; }

        public bool IsActive { get; set; }

        public ICollection<LanguageConcept> Concepts { get; set; }

    }
}