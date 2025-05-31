using E_Learning.GraduationProject.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class Student : BaseEntity<int>
    {

        public ICollection<StudentProgress>? studentProgresses { get; set; }

        // Navigation property
        public ApplicationUser User { get; set; } = null!;
        [Required]
        public string UserId { get; set; } = string.Empty; // Foreign key to ApplicationUser
    }
}
