using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Learning.GraduationProject.Core.Dtos.Instructors;
using E_Learning.GraduationProject.Core.Dtos.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Dtos.Tracks;

namespace E_Learning.GraduationProject.Core.Dtos.Courses
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } // picture UI
        public int? InstructorId { get; set; }
        public int? ProgrammingLanguageId { get; set; } // for updates
        public ProgrammingLanguageDto? ProgrammingLanguage { get; set; }
        public int? TrackId { get; set; } //for update
        public CreateTrackDto? Track { get; set; }

    }
}
