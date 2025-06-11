using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Courses
{
    public class CourseParams
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; } // [Price , Title ]

        public bool? IsPublished { get; set; }

        public int? InstructorId { get; set; }


        private string? search; // [title ]


        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


    }
}
