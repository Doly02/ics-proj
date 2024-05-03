using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Facades;

public interface ISubjectFacade : IFacade<SubjectEntity, SubjectListModel, SubjectDetailModel>
{
    Task<IEnumerable<SubjectListModel>> SearchAsync(string? Search = null);
    Task<IEnumerable<SubjectListModel>> GetSortedAsync(bool ascending, bool byName);
}

