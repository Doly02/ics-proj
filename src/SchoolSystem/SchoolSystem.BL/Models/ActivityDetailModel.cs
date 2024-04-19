using SchoolSystem.DAL.Enums;

namespace SchoolSystem.BL.Models
{
    public record ActivityDetailModel : ModelBase
    {
        public string? Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Place { get; set; }
        public required ActivityType ActivityType { get; set; }
        public string? Description { get; set;  }
        public string? SubjectName { get; set; }

        public static ActivityDetailModel Empty
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Start = DateTime.MinValue,
                End = DateTime.MinValue,
                Place = string.Empty,
                ActivityType = ActivityType.Other,
                Description = string.Empty,
                SubjectName = string.Empty
            };
    }
}


