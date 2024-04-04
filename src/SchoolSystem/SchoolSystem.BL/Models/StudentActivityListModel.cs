﻿using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Models
{
    public record StudentActivityListModel : ModelBase
    {
        public string? ActivityName { get; set; }
        public string? SubjectName { get; set; }
        public string? StudentFullName { get; set; }
        public int Score { get; set; }
        public required ObservableCollection<EvaluationDetailModel> Evaluation { get; set; }
        
        public static StudentActivityListModel Empty
            => new()
            {
                Id = Guid.Empty,
                ActivityName = string.Empty,
                StudentFullName = string.Empty,
                SubjectName = string.Empty,
                Evaluation = new ObservableCollection<EvaluationDetailModel>(),
                Score = 0
            };
    }
}