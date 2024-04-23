using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.Repositories;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class ActivityFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    ActivityModelMapper activityModelMapper)
    :
        FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel,
            ActivityEntityMapper>(unitOfWorkFactory, activityModelMapper), IActivityFacade
{
    public async Task SaveAsync(ActivityDetailModel model, Guid id)
    {
        var mapper = new ActivityModelMapper();
        ActivityEntity entity = mapper.MapToEntity(model, id);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository =
            uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        repository.InsertEntityAsync(entity);
        await uow.CommitAsync();
    }
    
    public async Task<ObservableCollection<ActivityListModel>> FilterActivitiesByTimeAsync(DateTime startDateTime, DateTime endDateTime)
    {
        var mapper = new ActivityModelMapper();
        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
        
        // the result of filtering
        var activities = await repository.Get()  // create query to filter 
            .Where(a => a.Start >= startDateTime && a.End <= endDateTime)
            .ToListAsync();

        // filtrated activity list
        var activityListModels = activities.Select(entity => mapper.MapToListModel(entity)).ToList();
  
        return new ObservableCollection<ActivityListModel>(activityListModels);
    }
    
    public ObservableCollection<ActivityListModel> SortActivitiesAscendingAsync(List<ActivityEntity> activities)
    {
        // Create an instance to map entities to models.
        var mapper = new ActivityModelMapper();
        // Store result of sorting list.
        var sortedActivities = activities
            .OrderBy(a => a.Name)
            .Select(mapper.MapToListModel) // convert entities to ActivityListModel
            .ToList(); // convert IEnumerable to List

        // Return observable collection.
        return new ObservableCollection<ActivityListModel>(sortedActivities);
    }

    public ObservableCollection<ActivityListModel> SortActivitiesDescendingAsync(List<ActivityEntity> activities)
    {
        var mapper = new ActivityModelMapper();
        var sortedActivities = activities
            .OrderByDescending(a => a.Name)
            .Select(mapper.MapToListModel)
            .ToList();

        return new ObservableCollection<ActivityListModel>(sortedActivities);
    }
}
