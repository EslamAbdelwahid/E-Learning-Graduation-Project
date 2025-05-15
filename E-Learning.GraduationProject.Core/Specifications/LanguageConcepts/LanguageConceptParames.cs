using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.LanguageConcepts
{
    public class LanguageConceptParames
    {

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Sort { get; set; }  // Asc Desc [orderIndex , Hours , title]

        private string? search;  // title
        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


    }
}
