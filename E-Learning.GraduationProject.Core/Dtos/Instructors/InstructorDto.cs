using E_Learning.GraduationProject.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Instructors
{
    public class InstructorDto
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Expertise { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
