using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.ProgrammingLanguages
{
    public class ProgrammingLanguageParames
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5; 
        public string? Sort { get; set; } // Name 

        public string? DifficultyLevel { get; set; } // [Basic , Intermediate , Advanced]

        private string? search; // Name

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
