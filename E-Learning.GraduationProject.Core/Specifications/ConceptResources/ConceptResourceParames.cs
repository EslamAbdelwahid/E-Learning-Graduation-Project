using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.ConceptResources
{
    public class ConceptResourceParames
    {
      
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Sort { get; set; } // orderby Asc Desc [Duration , title]

        private string? search; //search by title
       
        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


    }
}
