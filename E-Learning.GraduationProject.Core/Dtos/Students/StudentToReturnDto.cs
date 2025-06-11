using E_Learning.GraduationProject.Core.Dtos.Adresses;
using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Students
{
    public class StudentToReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } 
        public AddressToReturnDto? Address { get; set; }



    }
}
