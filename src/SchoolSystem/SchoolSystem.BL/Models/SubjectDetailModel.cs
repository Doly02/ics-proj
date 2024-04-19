namespace SchoolSystem.BL.Models
{
    public record SubjectDetailModel : ModelBase
    {
        public string? Name { get; set; }
        private string? _abbreviation;
        public string? Abbreviation
        {
            get => _abbreviation;
            set => _abbreviation = value?.ToUpper();
        }
        
        public static SubjectDetailModel Empty
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Abbreviation = string.Empty,
            };
    }
}
