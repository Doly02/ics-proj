namespace SchoolSystem.BL.Models
{
    public record ActivityListModel : ModelBase
    {
        public string? Name { get; set; }
        public string? StudentFullName { get; set; }
        
        public static ActivityListModel Empty
            => new()
            {
                Id = Guid.Empty,
                Name = string.Empty,
                StudentFullName = string.Empty
            };
    }
}