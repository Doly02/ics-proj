using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{

    Task SaveAsync(ActivityDetailModel model, Guid Id);
    Task<ObservableCollection<ActivityListModel>> FilterActivitiesByTimeAsync(DateTime startDateTime, DateTime endDateTime);
    Task<ObservableCollection<ActivityListModel>> SortActivitiesAscendingAsync();
    Task<ObservableCollection<ActivityListModel>> SortActivitiesDescendingAsync();


}
