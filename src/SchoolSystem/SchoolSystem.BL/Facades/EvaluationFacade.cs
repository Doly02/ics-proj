using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class EvaluationFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    EvaluationModelMapper evaluationModelMapper)
    :
        FacadeBase<EvaluationEntity, StudentActivityListModel, EvaluationDetailModel,
            EvaluationEntityMapper>(unitOfWorkFactory, evaluationModelMapper), IEvaluationFacade
{
    // Retrieves a detail model for a specific entity ID
    public override async Task<EvaluationDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork UnitOfWork = UnitOfWorkFactory.Create();

        IQueryable<EvaluationEntity> query = UnitOfWork.GetRepository<EvaluationEntity, EvaluationEntityMapper>().Get();

        EvaluationEntity? evalEntity = await query.SingleOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
        
        if (evalEntity is not null)
        {
            // Including student and activity
            IQueryable<StudentEntity> studQuery = UnitOfWork.GetRepository<StudentEntity, StudentEntityMapper>().Get();
            StudentEntity? studentEntity = await studQuery.SingleOrDefaultAsync(e => e.Id == evalEntity.StudentId).ConfigureAwait(false);
        
            IQueryable<ActivityEntity> activityQuery = UnitOfWork.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();
            ActivityEntity? activityEntity = await activityQuery.SingleOrDefaultAsync(e => e.Id == evalEntity.ActivityId).ConfigureAwait(false);
            
            if (studentEntity is not null && activityEntity is not null)
            {
                evalEntity.Student = studentEntity;
                evalEntity.Activity = activityEntity;
            }
        }

        return evalEntity is null
            ? null
            : ModelMapper.MapToDetailModel(evalEntity);
    }
}
