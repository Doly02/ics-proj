using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class EvaluationModelMapper 
    : ModelMapperBase<EvaluationEntity, ActivityEvaluationListModel, EvaluationDetailModel>, IEvaluationModelIMapper
{
    public override ActivityEvaluationListModel MapToListModel(EvaluationEntity? entity)
        => entity is null
            ? ActivityEvaluationListModel.Empty
            : new ActivityEvaluationListModel
            {
                
            };
    
    public override EvaluationDetailModel MapToDetailModel(EvaluationEntity? entity)
        => entity is null
            ? EvaluationDetailModel.Empty
            : new EvaluationDetailModel
            {
                
            };
    
    public override EvaluationEntity MapToEntity(EvaluationDetailModel model)
        => throw new NotImplementedException("This method is unsupported. Use the other overload.");
    
    
    public EvaluationEntity MapToEntity(EvaluationDetailModel model, Guid studentId)
        => new()
        {
            Score = model.Score,
            Note = model.Note,
            StudentId = studentId,
            ActivityId = model.Id,
            Student = null!,
            Activity = null!
        };
}
