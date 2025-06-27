using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.StudentProgresses
{
    public class EnrolledCourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public bool IsFavorite { get; set; }
    }
}
