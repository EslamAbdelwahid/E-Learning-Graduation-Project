using E_Learning.GraduationProject.Core.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Instructors
{
    public class InstructorSpecifications :BaseSpecification<Instructor,int>
    {
        public InstructorSpecifications(InstructorParams specParams): base(
            I =>
            (string.IsNullOrEmpty(specParams.Search) || I.User.FirstName.ToLower().Contains(specParams.Search) || I.User.LastName.ToLower().Contains(specParams.Search))
            )
        {
            ApplyIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "Years_Asc":
                        AddOrderBy(I => I.YearsOfExperience);
                        break;
                    case "Years_Desc":
                        AddOrderByDesc(I => I.YearsOfExperience);
                        break;
                    case "Total_Students_Asc":
                        AddOrderBy(I => I.TotalStudents);
                        break;
                    case "Total_Students_Desc":
                        AddOrderByDesc(I => I.TotalStudents);
                        break;
                    case "Total_Courses_Asc":
                        AddOrderBy(I => I.TotalCourses);
                        break;
                    case "Total_Courses_Desc":
                        AddOrderByDesc(I => I.TotalCourses);
                        break;
                    case "First_Name_Asc":
                        AddOrderBy(I => I.User.FirstName);
                        break;
                    case "First_Name_Desc":
                        AddOrderByDesc(I => I.User.FirstName);
                        break;

                    default:
                        AddOrderBy(I => I.User.FirstName);
                        break;
                    
                }
            }
            ApplyPagination(specParams.PageSize, specParams.PageSize * (specParams.PageIndex - 1));

        }

        public InstructorSpecifications(int id):base(
            I => I.Id == id
            )
        {
            ApplyIncludes();
        }
        public InstructorSpecifications()
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(I => I.User);
            Includes.Add(I => I.Courses);
        }
    }
}
