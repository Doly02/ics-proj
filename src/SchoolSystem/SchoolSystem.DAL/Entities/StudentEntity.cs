namespace SchoolSystem.DAL.Entities
{
    public record StudentEntity : IEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        
        public ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();
        public ICollection<EnrolledEntity> Enrolleds{ get; set; } = new List<EnrolledEntity>();
    }
}
