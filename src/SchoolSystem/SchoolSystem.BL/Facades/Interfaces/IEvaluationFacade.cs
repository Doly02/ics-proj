using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

public interface IEvaluationFacade : IFacade<EvaluationEntity, StudentActivityListModel, EvaluationDetailModel>
{
    //Task<EvaluationDetailModel?> GetEmptyModel(Guid activityId, Guid studentId);
    Task<EvaluationDetailModel> GetAsync(Guid studentId, Guid activityId);
}
