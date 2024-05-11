using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Mappers;
using SchoolSystem.DAL.Repositories;
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
    /*public override async Task<EvaluationDetailModel?> GetAsync(Guid id)
    {
         throw new NotImplementedException("Use the other override");
    }*/

    public async Task<EvaluationDetailModel> GetAsync(Guid studentId, Guid activityId)
    {
        await using IUnitOfWork UnitOfWork = UnitOfWorkFactory.Create();
    
        // trying to get existing activity
        IQueryable<EvaluationEntity> evalQuery =
            UnitOfWork.GetRepository<EvaluationEntity, EvaluationEntityMapper>().Get();
        EvaluationEntity? evaluationEntity = await evalQuery
            .SingleOrDefaultAsync(e => e.StudentId == studentId && e.ActivityId == activityId)
            .ConfigureAwait(false);

        // Getting existing entities from id
        IQueryable<StudentEntity> studQuery =
            UnitOfWork.GetRepository<StudentEntity, StudentEntityMapper>().Get();
        StudentEntity? studentEntity = await studQuery.SingleOrDefaultAsync(e => e.Id == studentId)
            .ConfigureAwait(false);

        IQueryable<ActivityEntity> activityQuery =
            UnitOfWork.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();
        ActivityEntity? activityEntity = await activityQuery
            .SingleOrDefaultAsync(e => e.Id == activityId).ConfigureAwait(false);
        
        if (studentEntity is not null && activityEntity is not null)
        {
            if (evaluationEntity is not null)
            {
                evaluationEntity.Student = studentEntity;
                evaluationEntity.Activity = activityEntity;
                return ModelMapper.MapToDetailModel(evaluationEntity);
            }
            // Not found, empty model
            EvaluationEntity evalEntity = new()
            {
                Id = Guid.Empty,
                Activity = activityEntity,
                ActivityId = activityId,
                Student = studentEntity,
                StudentId = studentId
            };
            return ModelMapper.MapToDetailModel(evalEntity);
        }

        return EvaluationDetailModel.Empty;
    }
    
    public override async Task<EvaluationDetailModel> SaveAsync(EvaluationDetailModel model)
    {
        EvaluationDetailModel res;

        EvaluationEntity entity = ModelMapper.MapToEntity(model);

        IUnitOfWork UnitOfWork = UnitOfWorkFactory.Create();
        
        // updating/inserting evaluation 
        IRepository<EvaluationEntity> evalRepository = UnitOfWork.GetRepository<EvaluationEntity, EvaluationEntityMapper>();
        
        if (await evalRepository.ExistsEntityAsync(entity).ConfigureAwait(false))
        {
            EvaluationEntity updatedEntity = await evalRepository.UpdateEntityAsync(entity).ConfigureAwait(false);
            res = ModelMapper.MapToDetailModel(updatedEntity);
            res.ActivityName = model.ActivityName;
            res.StudentFullName = model.StudentFullName;
        }
        else
        {
            entity.Id = Guid.NewGuid();
            EvaluationEntity insertedEntity = evalRepository.InsertEntityAsync(entity);
            res = ModelMapper.MapToDetailModel(insertedEntity);
        }
        
        // needed for updating student and activity
        /*IRepository<StudentEntity> studRepository = UnitOfWork.GetRepository<StudentEntity, StudentEntityMapper>();
        IRepository<ActivityEntity> actRepository = UnitOfWork.GetRepository<ActivityEntity, ActivityEntityMapper>();

        IQueryable<StudentEntity> studQuery = UnitOfWork.GetRepository<StudentEntity, StudentEntityMapper>().Get();
        StudentEntity? studentEntity = await studQuery.SingleOrDefaultAsync(e => e.Id == entity.StudentId).ConfigureAwait(false);
        
        IQueryable<ActivityEntity> activityQuery = UnitOfWork.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();
        ActivityEntity? activityEntity = await activityQuery.SingleOrDefaultAsync(e => e.Id == entity.ActivityId).ConfigureAwait(false);
        
        // update collection of student and activity
        bool foundInStud = false;
        bool foundInActivity = false;
        if (studentEntity is not null && activityEntity is not null)
        {
            // student
            foreach (var evaluation in studentEntity.Evaluations)
            {
                if(evaluation.Id == entity.Id)
                    foundInStud = true;
            }
            if (foundInStud)
            {
                studentEntity.Evaluations.Remove(entity);
                studentEntity.Evaluations.Add(entity);
            }
            else
                studentEntity.Evaluations.Add(entity);
            
            // activity
            foreach (var evaluation in activityEntity.Evaluations)
            {
                if(evaluation.Id == entity.Id)
                    foundInActivity = true;
            }
            if (foundInActivity)
            {
                activityEntity.Evaluations.Remove(entity);
                activityEntity.Evaluations.Add(entity);
            }
            else
                activityEntity.Evaluations.Add(entity);
            
            // update db
            await studRepository.UpdateEntityAsync(studentEntity).ConfigureAwait(false);
            await actRepository.UpdateEntityAsync(activityEntity).ConfigureAwait(false);
        }*/
        
        await UnitOfWork.CommitAsync().ConfigureAwait(false);

        return res;
    }
}
