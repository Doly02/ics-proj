using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Models
{
    public record ActivityListModel : ModelBase
    {
        public string? ActivityName { get; set; }
        public string? SubjectName { get; set; }
        public required ObservableCollection<ActivityDetailModel> Activity { get; set; }
        public int Score { get; set; }
        
        public static ActivityListModel Empty
            => new()
            {
                Id = Guid.Empty,
                ActivityName = string.Empty,
                SubjectName = string.Empty,
                Activity = new ObservableCollection<ActivityDetailModel>(),
                Score = 0
            };
    }
}