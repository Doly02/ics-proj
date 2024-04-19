using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Models
{
    public record StudentDetailModel : ModelBase
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public required ObservableCollection<EnrolledSubjectsListModel> EnrolledSubjects { get; set; }

        public static StudentDetailModel Empty
            => new()
            {
                Id = Guid.Empty,
                Name = string.Empty,
                Surname = string.Empty,
                ImageUrl = null,
                EnrolledSubjects = new ObservableCollection<EnrolledSubjectsListModel>()
            };
    }
}
