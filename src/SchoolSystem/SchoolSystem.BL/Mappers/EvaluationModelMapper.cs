using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class EvaluationModelMapper 
    : ModelMapperBase<EvaluationEntity, StudentActivityListModel, EvaluationDetailModel>
{
    public override StudentActivityListModel MapToListModel(EvaluationEntity? entity)
        => entity is null
            ? StudentActivityListModel.Empty
            : new StudentActivityListModel
            {
                Id = entity.Id,
                ActivityName = entity.Activity?.Name,
                Score = entity.Score,
                StudentFullName = entity.Student?.Name,
                SubjectName = entity.Activity?.Subject?.Name,
                Evaluation = [MapToDetailModel(entity)]
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
                StudentFullName = entity.Student?.Name + " " + entity.Student?.Surname,
                ActivityName = entity.Activity?.Name
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
