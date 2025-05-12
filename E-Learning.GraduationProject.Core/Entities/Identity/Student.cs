using E_Learning.GraduationProject.Core.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace E_Learning.GraduationProject.Core.Entities.Identity
{
    public class Student : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool IsActive { get; set; }
        public ICollection<StudentProgress> Progresses { get; set; }
    }
}