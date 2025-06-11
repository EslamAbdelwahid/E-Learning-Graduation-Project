using E_Learning.GraduationProject.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Instructors
{
    public class InstructorToReturnDto
    {
        [JsonPropertyName("InstructorId")]
        public int Id { get; set; }
        public string UserId { get; set; } // Auth One
        public DateTimeOffset CreatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address? Address { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Expertise { get; set; }
        public int YearsOfExperience { get; set; }
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public bool IsVerified { get; set; }

    }
}
