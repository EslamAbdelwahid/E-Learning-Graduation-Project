namespace E_Learning.GraduationProject.Core.Entities.Instructors
{
    public class Course : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } // picture UI
        public bool IsPublished { get; set; }

        // Relationships
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        public int? ProgrammingLanguageId { get; set; }
        public ProgrammingLanguage? ProgrammingLanguage { get; set; }

        public int? TrackId { get; set; }
        public Track? Track { get; set; }

        public ICollection<StudentProgress>? StudentProgresses { get; set; } 
    }
}