using E_Learning.GraduationProject.Core.Entities.Enums;

namespace E_Learning.GraduationProject.Core.Entities
{
    public class PractiseProblem : BaseEntity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ExternalUrl { get; set; }

        public PlatformName PlatformName { get; set; }

        public ProblemDifficultyLevel ProblemDifficultyLevel { get; set; }

        public int EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public int OrderIndex { get; set; }

        public bool IsActive { get; set; }

    }
}