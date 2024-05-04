using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Models
{
    public record EnrolledSubjectsListModel : ModelBase
    {
        public string? Name { get; set; }
        private string? _abbreviation;
        public Guid SubjectId { get; set; }

        public string? Abbreviation
        {
            get => _abbreviation;
            set => _abbreviation = value?.ToUpper();
        }

        public string? StudentFullName { get; set; }
        public required ObservableCollection<ActivityListModel> Activities { get; set; }

        public static EnrolledSubjectsListModel Empty
            => new()
            {
                Id = Guid.Empty,
                SubjectId = Guid.Empty,
                Name = string.Empty,
                Abbreviation = string.Empty,
                StudentFullName = string.Empty,
                Activities = new ObservableCollection<ActivityListModel>()
            };
    }
}
