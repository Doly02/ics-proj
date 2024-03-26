namespace SchoolSystem.BL.Models
{
    public record SubjectListModel : ModelBase
    {
        public string? Name { get; set; }
        private string? _abbreviation;
        public string? Abbreviation
        {
            get => _abbreviation;
            set => _abbreviation = value?.ToUpper();
        }
        public string? StudentFullName { get; set; }
        
        public static SubjectListModel Empty
            => new()
            {
                Id = Guid.Empty,
                Name = string.Empty,
                Abbreviation = string.Empty,
                StudentFullName = string.Empty
            };
    }
}


