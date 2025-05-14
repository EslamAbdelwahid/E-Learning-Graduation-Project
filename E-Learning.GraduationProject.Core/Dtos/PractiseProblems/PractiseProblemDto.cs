using E_Learning.GraduationProject.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Dtos.PractiseProblems
{
    public class PractiseProblemDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ExternalUrl { get; set; }

        public string PlatformName { get; set; }

        public string ProblemDifficultyLevel { get; set; }

        public int EntityId { get; set; }

        public string EntityType { get; set; }

        public int OrderIndex { get; set; }

        public bool IsActive { get; set; }
    }
}
