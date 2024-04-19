using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

public interface IEnrolledFacade : IListModelFacade<EnrolledEntity, EnrolledSubjectsListModel>
{
    Task<EnrolledSubjectsListModel?> GetByIdAsync(Guid id);
}
