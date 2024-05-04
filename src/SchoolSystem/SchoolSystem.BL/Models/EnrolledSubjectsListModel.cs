using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Models
{
    public record EnrolledSubjectsListModel : ModelBase
    {

        public required Guid SubjectId { get; set; }
        public required string SubjectName { get; set; }

        
        public string? StudentFullName { get; set; }
        public required ObservableCollection<ActivityListModel> Activities { get; set; }

        public static EnrolledSubjectsListModel Empty
            => new()
            {
                Id = Guid.Empty,
                SubjectId = Guid.Empty,
                SubjectName = string.Empty,
                StudentFullName = string.Empty,
                Activities = new ObservableCollection<ActivityListModel>()
            };
    }
}
