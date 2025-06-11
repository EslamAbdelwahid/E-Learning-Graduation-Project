using E_Learning.GraduationProject.Core.Entities.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Specifications.Instructors
{
    public class InstructorWithCountSpecifications : BaseSpecification<Instructor,int>
    {
        public InstructorWithCountSpecifications(InstructorParams specParams) : base(
           I =>
           (string.IsNullOrEmpty(specParams.Search) || I.User.FirstName.ToLower().Contains(specParams.Search) || I.User.LastName.ToLower().Contains(specParams.Search))
           )
        {

        }
    }
}
