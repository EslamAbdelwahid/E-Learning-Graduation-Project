using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name is Required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is Required!")]

        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required!")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is Required!")]
        public AddressDto AddressDto { get; set; }
    }
}
