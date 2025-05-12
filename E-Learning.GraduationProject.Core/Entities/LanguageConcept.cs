using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class LanguageConcept : BaseEntity<int>
    {
        [JsonPropertyName("conceptId")]
        public override int Id { get; set; }

        [JsonPropertyName("languageId")]
        public int ProgrammingLanguageId { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int OrderIndex { get; set; }

        public int? EstimatedHours { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public ProgrammingLanguage ProgrammingLanguage { get; set; }

        public ICollection<ConceptResource> Resources { get; set; }

        public ICollection<PractiseProblem> Problems { get; set; }
    }
}
