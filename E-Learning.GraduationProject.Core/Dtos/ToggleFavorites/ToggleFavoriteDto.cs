using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.ToggleFavorites
{
    public class ToggleFavoriteDto
    {
        [Required]
        public int StudentId{ get; set; }
        [Required]
        public int CourseId{ get; set; }
    }
}
