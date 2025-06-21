using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Students
{
    public class StudentEnrollmentsWithCourseSpec : BaseSpecification<StudentProgress, int>
    {
        public StudentEnrollmentsWithCourseSpec(int studentId)
            : base(sp => sp.StudentId == studentId)
        {
            Includes.Add(sp => sp.Course);
            Includes.Add(sp => sp.Course!.Instructor); 
            Includes.Add(sp => sp.Course!.ProgrammingLanguage);
            Includes.Add(sp => sp.Course!.Track);
        }
    }
}
