namespace SchoolSystem.BL.Models
{
    public record EvaluationDetailModel : ModelBase
    {
        public int Score { get; set; }
        public string? Note { get; set; }
        
        public string? StudentFullName { get; set; }
        public string? ActivityName { get; set; }

        public static EvaluationDetailModel Empty
            => new()
            {
                Id = Guid.NewGuid(),
                Score = 0,
                Note = string.Empty,
                StudentFullName = string.Empty,
                ActivityName = string.Empty
            };
    } 
}
