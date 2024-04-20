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
    public async Task SaveAsync(ActivityDetailModel model, Guid Id)
    {
        var mapper = new ActivityModelMapper();
        ActivityEntity entity = mapper.MapToEntity(model, Id);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository =
            uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        repository.InsertEntityAsync(entity);
        await uow.CommitAsync();
    }
}