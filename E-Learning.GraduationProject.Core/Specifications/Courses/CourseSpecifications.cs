using E_Learning.GraduationProject.Core.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Courses
{
    public class CourseSpecifications : BaseSpecification<Course, int>
    {
        public CourseSpecifications(CourseParams specParams) : base(
            C =>
            (string.IsNullOrEmpty(specParams.Search) || C.Title.ToLower().Contains(specParams.Search) )
            &&
            (specParams.IsPublished == null || C.IsPublished == specParams.IsPublished)
            &&
            (specParams.InstructorId == null || C.InstructorId == specParams.InstructorId)
            )
        {

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "Price_Asc":
                        AddOrderBy(C => C.Price);
                        break;
                    case "Price_Desc":
                        AddOrderByDesc(C => C.Price);
                        break;
                    case "Title_Asc":
                        AddOrderBy(C => C.Title);
                        break;
                    case "Title_Desc":
                        AddOrderByDesc(C => C.Title);
                        break;
                    default:
                        AddOrderBy(C => C.Title);
                        break;

                }
            }

            ApplyPagination(specParams.PageSize, specParams.PageSize * (specParams.PageIndex - 1));

            ApplyIncludes();
        }
        public CourseSpecifications(int id ) : base(
            C => C.Id == id
            )
        {
            ApplyIncludes();
        }


        private void ApplyIncludes()
        {
            Includes.Add(C => C.Instructor);
            Includes.Add(C => C.ProgrammingLanguage);
            Includes.Add(C => C.StudentProgresses);

        }
    }
}
