using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Facades;

public interface IStudentFacade : IFacade<StudentEntity, StudentListModel, StudentDetailModel>
{
    Task<ObservableCollection<StudentListModel>> GetStudentsSortedBySurnameAscendingAsync(bool name);

    Task<ObservableCollection<StudentListModel>> GetStudentsSortedBySurnameDescendingAsync(bool name);

    public Task<IEnumerable<StudentListModel>> SearchAsync(string? Name = null,
        Guid? Id = null);
}
