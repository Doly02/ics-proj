using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class EvaluationModelMapper 
    : ModelMapperBase<EvaluationEntity, ActivityEvaluationListModel, EvaluationDetailModel>
{
    public override ActivityEvaluationListModel MapToListModel(EvaluationEntity? entity)
        => entity is null
            ? ActivityEvaluationListModel.Empty
            : new ActivityEvaluationListModel
            {
                Id = entity.Id,
                ActivityName = entity.Activity.Name,
                EvaluationScore = entity.Score,
                StudentFullName = entity.Student.Name,
                SubjectName = entity.Activity.Subject.Name
            };
    
    public override EvaluationDetailModel MapToDetailModel(EvaluationEntity? entity)
        => entity is null
            ? EvaluationDetailModel.Empty
            : new EvaluationDetailModel
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                ActivityId = entity.ActivityId,
                Score = entity.Score,
                Note = entity.Note,
                StudentFullName = entity.Student.Name,
                ActivityName = entity.Activity.Name
            };
    
    public override EvaluationEntity MapToEntity(EvaluationDetailModel model)
        => new()
        {
            Id = model.Id,
            Score = model.Score,
            Note = model.Note,
            StudentId = model.StudentId,
            ActivityId = model.ActivityId,
            Student = null!,
            Activity = null!
        };
}
