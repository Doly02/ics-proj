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
    
    public async Task<ObservableCollection<ActivityListModel>> SortActivitiesAscendingAsync()
    {
        // Create an instance to map entities to models.
        var mapper = new ActivityModelMapper();
        
        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
        
        var sortedActivities = await repository.Get()
            .OrderBy(a => a.Name)
            .ToListAsync();

        return new ObservableCollection<ActivityListModel>(sortedActivities.Select(mapper.MapToListModel));
    }

    public async Task<ObservableCollection<ActivityListModel>> SortActivitiesDescendingAsync()
    {
        var mapper = new ActivityModelMapper();
    
        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
    
        var sortedActivities = await repository.Get()
            .OrderByDescending(a => a.Name)
            .ToListAsync();

        return new ObservableCollection<ActivityListModel>(sortedActivities.Select(mapper.MapToListModel));
    }
}
