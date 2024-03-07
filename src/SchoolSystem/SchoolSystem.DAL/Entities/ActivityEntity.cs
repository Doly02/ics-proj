using SchoolSystem.DAL.Enums;

namespace SchoolSystem.DAL.Entities
{
    public record ActivityEntity : IEntity
    {
        public required Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Place { get; set; }
        public required ActivityType ActivityType { get; set; }
        public string? Description { get; set;  }
        
        public required SubjectEntity Subject { get; set; }
        public ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();
    }
}