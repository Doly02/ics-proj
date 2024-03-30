using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

// Do Not Inherits From IEntity
public interface IEvaluationFacade
{
    Task SaveAsync(EvaluationDetailModel model, Guid recipeId);
    Task DeleteAsync(Guid id);
}
