using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.StudentFavoriteCourses
{
    public class StudentFavoriteCoursesSpec : BaseSpecification<StudentCourseFavorite,int>
    {
        public StudentFavoriteCoursesSpec(int studentId) :base(
            SV => SV.StudentId == studentId
            )
        {
            ApplyIncludes();
        }

        public StudentFavoriteCoursesSpec(int studentId , int courseId) : base(
            SV => SV.StudentId == studentId && SV.CourseId == courseId
            )
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(SV => SV.Course);
            Includes.Add(SV => SV.Course.Instructor);
            Includes.Add(SV => SV.Course.ProgrammingLanguage);
            Includes.Add(SV => SV.Course.Track);
        }

    }
}
