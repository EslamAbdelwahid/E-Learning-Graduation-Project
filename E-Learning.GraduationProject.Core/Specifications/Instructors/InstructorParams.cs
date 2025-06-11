using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Instructors
{
    public class InstructorParams
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Sort { get; set; } // [YearsOfExperience , TotalStudents , TotalCourses, FirstName]

        private string? search; // [Name]

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }



    }
}
