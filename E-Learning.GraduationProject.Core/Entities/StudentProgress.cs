using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class StudentProgress : BaseEntity<int>
    {
        public string StudentId { get; set; }

        public Student Student { get; set; }

        public int EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public ProgressStatus Status { get; set; }

        public DateTime LastWatchedDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int? TimeSpent { get; set; }

        public int CurrentPositionSeconds { get; set; }



    }
}
