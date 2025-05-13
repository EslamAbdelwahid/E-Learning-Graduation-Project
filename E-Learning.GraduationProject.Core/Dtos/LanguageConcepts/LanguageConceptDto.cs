using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.LanguageConcepts
{
    public class LanguageConceptDto
    {
        public int? ProgrammingLanguageId { get; set; }  // languageId that linked with this concept

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int OrderIndex { get; set; }

        public int? EstimatedHours { get; set; }

        public string DifficultyLevel { get; set; }

        public ICollection<ConceptResource>? Resources { get; set; }

        public ICollection<PractiseProblem>? Problems { get; set; }
    }
}
