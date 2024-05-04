using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

public interface IEnrolledFacade : IFacade<EnrolledEntity, EnrolledSubjectsListModel, EnrolledDetailModel>
{
    Task<EnrolledSubjectsListModel?> GetByIdAsync(Guid id);

    Task SaveAsync(EnrolledSubjectsListModel model, Guid id);

    Task SaveAsync(EnrolledDetailModel model, Guid id);
    Task<IEnumerable<EnrolledSubjectsListModel>> SearchBySubjectNameAsync(string? subjectName = null);
    Task<IEnumerable<EnrolledSubjectsListModel>> SortEnrolledSubjectsAscAsync();
}
