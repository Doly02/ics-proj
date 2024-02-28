namespace SchoolSystem.DAL.Entities
{
    public record StudentEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        
        public ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
        public ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();
    }
}
