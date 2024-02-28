namespace SchoolSystem.DAL.Entities
{
    public record SubjectEntity : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        private string? _abbreviation;

        public string? Abbreviation
        {
            get => _abbreviation;
            set => _abbreviation = value?.ToUpper();
        }
        
        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
        public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    }
}
