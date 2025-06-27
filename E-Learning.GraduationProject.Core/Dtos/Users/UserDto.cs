using E_Learning.GraduationProject.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Users
{
    public class UserDto
    {
        [JsonPropertyName("authId")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("entityId")]
        public int? EntityId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();

        public List<string>? Roles { get; set; }
    }
}
