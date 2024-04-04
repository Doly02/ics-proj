namespace SchoolSystem.BL.Models
{
    public record EnrolledSubjectsListModel : ModelBase
    {
        public string? Name { get; set; }
        private string? _abbreviation;

        public string? Abbreviation
        {
            get => _abbreviation;
            set => _abbreviation = value?.ToUpper();
        }
        
        public string? StudentFullName { get; set; }
        public required List<ActivityListModel> Activities { get; set; }

        public static EnrolledSubjectsListModel Empty
            => new()
            {
                Id = Guid.Empty,
                Name = string.Empty,
                Abbreviation = string.Empty,
                StudentFullName = string.Empty,
                Activities = new List<ActivityListModel>()
            };
    }
}
