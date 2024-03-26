namespace SchoolSystem.BL.Models
{
    public record StudentDetailModel : ModelBase
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }

        public static StudentDetailModel Empty
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Surname = string.Empty,
                ImageUrl = string.Empty
            };
    }
}


