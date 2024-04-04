namespace SchoolSystem.BL.Models
{
    public record ActivityListModel : ModelBase
    {
        public string? ActivityName { get; set; }
        public string? SubjectName { get; set; }
        public required List<ActivityDetailModel> Activity { get; set; }
        
        public static ActivityListModel Empty
            => new()
            {
                Id = Guid.Empty,
                ActivityName = string.Empty,
                SubjectName = string.Empty,
                Activity = new List<ActivityDetailModel>()
            };
    }
}