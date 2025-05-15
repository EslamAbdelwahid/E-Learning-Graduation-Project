using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.ProgrammingLanguages
{
    public class ProgrammingLanguageDto
    {
      
        public string Name { get; set; }

        public string Description { get; set; }

        public string IconUrl { get; set; }

        public string Difficulty { get; set; }

        public bool IsActive { get; set; }

        public ICollection<LanguageConcept>? Concepts { get; set; }
    }
}
