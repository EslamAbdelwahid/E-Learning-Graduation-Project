using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Resources
{
    public class ConceptResourceToReturn
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int? LanguageConceptId { get; set; }
        public LanguageConcept? LanguageConcept { get; set; }

        public string Url { get; set; }

        public string ResourceType { get; set; }
        public int TotalDurationMinutes { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
