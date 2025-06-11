using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Learning.GraduationProject.Core.Dtos.Courses;
using E_Learning.GraduationProject.Core.Dtos.Students;
using System.Text.Json.Serialization;

namespace E_Learning.GraduationProject.Core.Dtos.StudentProgresses
{
    public class StudentProgressToReturnDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } 
        public DateTimeOffset? UpdatedAt { get; set; }

        // student
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        // course
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int? EntityId { get; set; }
        public EntityType? EntityType { get; set; }

        public ProgressStatus Status { get; set; } 

        public DateTime LastWatchedDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int? TimeSpent { get; set; } //total minutes spent

        public int CurrentPositionSeconds { get; set; }
    }
}
