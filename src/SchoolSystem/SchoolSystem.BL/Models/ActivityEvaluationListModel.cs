namespace SchoolSystem.BL.Models
{
    public record ActivityEvaluationListModel : ModelBase
    {
        public string? ActivityName { get; set; }
        public int EvaluationScore { get; set; }
        
        public string? StudentFullName { get; set; }
        public string? SubjectName { get; set; }
        
        public static ActivityEvaluationListModel Empty
            => new()
            {
                Id = Guid.Empty,
                ActivityName = string.Empty,
                EvaluationScore = 0,
                StudentFullName = string.Empty,
                SubjectName = string.Empty
            };
    }
}


