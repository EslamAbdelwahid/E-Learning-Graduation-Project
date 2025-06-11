using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Dtos.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Dtos.StudentProgresses;
using E_Learning.GraduationProject.Core.Dtos.Students;
using E_Learning.GraduationProject.Core.Dtos.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.Courses
{
    public class CourseToReturnDto
    {
        [JsonPropertyName("CourseId")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsPublished { get; set; }

        public int? InstructorId { get; set; }
        
        public int? ProgrammingLanguageId { get; set; }
        
        public int? TrackId { get; set; }
        

        public ICollection<StudentProgressToReturnDto>? StudentProgresses { get; set; }
    }
}
