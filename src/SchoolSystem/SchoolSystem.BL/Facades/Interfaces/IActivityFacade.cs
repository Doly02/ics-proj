using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{

    Task SaveAsync(ActivityDetailModel model, Guid subjectId);
    Task<IEnumerable<ActivityListModel>> GetActivitiesAsync(Guid subjectId);
    Task<ObservableCollection<ActivityListModel>> FilterActivitiesByTimeAsync(DateTime startDateTime, DateTime endDateTime, Guid subjectId);
    Task<ObservableCollection<ActivityListModel>> SortActivitiesAscendingAsync(Guid subjectId);
    Task<ObservableCollection<ActivityListModel>> SortActivitiesDescendingAsync(Guid subjectId);

    Task<IEnumerable<ActivityListModel>> GetActivitiesWithEvalAsync(Guid subjectId, Guid studentId);


}
