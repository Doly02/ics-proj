namespace SchoolSystem.DAL.Entities
{
    public record EvaluationEntity
    {
        public int Score { get; set; }
        public string? Note { get; set; }

        // Id of evaluation defined by StudentId and ActivityId
        public required Guid StudentId { get; set; }
        public required StudentEntity Student { get; set; }

        public required Guid ActivityId { get; set; }
        public required ActivityEntity Activity { get; set; }
    }
}
