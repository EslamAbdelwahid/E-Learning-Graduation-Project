using E_Learning.GraduationProject.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities.Instructors
{
    public class Instructor : BaseEntity<int>
    {
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Expertise { get; set; }
        public int YearsOfExperience { get; set; }
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public bool IsVerified { get; set; }

        // Relationship to ApplicationUser
        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public ICollection<Course>? Courses { get; set; } 
    }
}
