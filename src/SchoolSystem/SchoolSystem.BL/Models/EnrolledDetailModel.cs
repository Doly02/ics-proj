using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Models
{
    public record EnrolledDetailModel : ModelBase
    {
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; } // Předpokládá se kombinace jména a příjmení
        public Guid SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public ObservableCollection<ActivityDetailModel> Activities { get; set; } = new ObservableCollection<ActivityDetailModel>();

        public static EnrolledDetailModel Empty
            => new()
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.Empty,
                StudentName = string.Empty,
                SubjectId = Guid.Empty,
                SubjectName = string.Empty,
                Activities = new ObservableCollection<ActivityDetailModel>()
            };
    }
}
