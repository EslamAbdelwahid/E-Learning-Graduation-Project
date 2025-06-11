using E_Learning.GraduationProject.Core.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Courses
{
    public class CourseWithCountSpecifications : BaseSpecification<Course, int>
    {
        public CourseWithCountSpecifications(CourseParams specParams) : base(
           C =>
           (string.IsNullOrEmpty(specParams.Search) || C.Title.ToLower().Contains(specParams.Search))
           &&
           (specParams.IsPublished == null || C.IsPublished == specParams.IsPublished)
           )
        {

        }
    }
}
