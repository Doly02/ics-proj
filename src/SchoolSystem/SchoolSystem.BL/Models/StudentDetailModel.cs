namespace SchoolSystem.BL.Models
{
    public record StudentDetailModel : ModelBase
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public required List<EnrolledSubjectsListModel> EnrolledSubjects { get; set; }

        public static StudentDetailModel Empty
            => new()
            {
                Id = Guid.Empty,
                Name = string.Empty,
                Surname = string.Empty,
                ImageUrl = null,
                EnrolledSubjects = new List<EnrolledSubjectsListModel>()
            };
    }
}
