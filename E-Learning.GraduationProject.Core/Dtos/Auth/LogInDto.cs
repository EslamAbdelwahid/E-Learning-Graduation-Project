using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Auth
{
    public class LogInDto
    {
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
 
    }
}
