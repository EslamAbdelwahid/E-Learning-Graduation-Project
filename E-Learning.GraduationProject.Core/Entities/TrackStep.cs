namespace E_Learning.GraduationProject.Core.Entities
{
    public class TrackStep : BaseEntity<int>
    {
        public int TrackId { get; set; }
        public Track Track { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int OrderIndex { get; set; }

        public int? EstimatedHours { get; set; }

        public bool IsRequired { get; set; }


        public ICollection<StepResource>? Resources { get; set; }

        public ICollection<PractiseProblem>? Problems { get; set; }

    }
}