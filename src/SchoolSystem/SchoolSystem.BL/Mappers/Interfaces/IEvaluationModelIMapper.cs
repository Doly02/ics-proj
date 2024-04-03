using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public interface IEvaluationModelIMapper
    : IModelMapper<EvaluationEntity, ActivityEvaluationListModel, EvaluationDetailModel>
{
    EvaluationEntity MapToEntity(EvaluationDetailModel model, Guid studentId); // for student id
}
