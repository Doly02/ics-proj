using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

public interface IEnrolledFacade : IFacade<EnrolledEntity, EnrolledSubjectsListModel, EnrolledDetailModel>
{
    Task<EnrolledSubjectsListModel?> GetByIdAsync(Guid id);

    Task SaveAsync(EnrolledSubjectsListModel model, Guid id);

    Task SaveAsync(EnrolledDetailModel model, Guid id);
    Task<IEnumerable<EnrolledSubjectsListModel>> GetEnrolledSubjectsByStudentIdAsync(Guid studentId);
    Task<IEnumerable<EnrolledSubjectsListModel>> SearchBySubjectNameAsync(Guid studentId, string? subjectName = null);
    Task<IEnumerable<EnrolledSubjectsListModel>> GetSortedAsync(bool ascending, bool byName, Guid studentId);
}
