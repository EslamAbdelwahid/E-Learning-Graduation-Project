using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Identity;
using E_Learning.GraduationProject.Core.Entities.Instructors;
using E_Learning.GraduationProject.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class StudentProgress : BaseEntity<int>
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int EntityId { get; set; }
        public EntityType EntityType { get; set; }
        public ProgressStatus Status { get; set; } = ProgressStatus.NotStarted;


        public DateTime? CompletionDate { get; set; }

        public int ProgressPercentage { get; set; } = 0;
        public bool IsCompleted { get; set; } = false;
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        public int? OrderId { get; set; } 
        public Order? Order { get; set; }  // nullable if some enrollments are free

    }
}
