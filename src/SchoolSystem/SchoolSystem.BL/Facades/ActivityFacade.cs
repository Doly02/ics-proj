using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class ActivityFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    ActivityModelMapper activityModelMapper)
    :
        FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel,
            ActivityEntityMapper>(unitOfWorkFactory, activityModelMapper);
