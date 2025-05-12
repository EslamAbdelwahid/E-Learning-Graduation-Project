using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos
{
    public class ConceptResourceDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        
        public int LanguageConceptId { get; set; }

        public string ConceptName { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int TotalDurationMinutes { get; set; }

        public DateTimeOffset CreatedAt { get; set; } 
    }
}
