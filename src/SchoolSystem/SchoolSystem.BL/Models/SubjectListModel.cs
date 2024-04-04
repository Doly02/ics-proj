using System.Collections.ObjectModel;

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

        public required ObservableCollection<ActivityListModel> Activities { get; set; }

        public static SubjectListModel Empty
            => new()
            {
                Id = Guid.Empty,
                Name = string.Empty,
                Abbreviation = string.Empty,
                Activities = new ObservableCollection<ActivityListModel>()
            };
    }
}


