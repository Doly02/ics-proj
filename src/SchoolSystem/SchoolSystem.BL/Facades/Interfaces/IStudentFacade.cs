using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;
using System.Collections.ObjectModel;

namespace SchoolSystem.BL.Facades;

public interface IStudentFacade : IFacade<StudentEntity, StudentListModel, StudentDetailModel>
{
    Task<ObservableCollection<StudentListModel>> GetStudentsSortedBySurnameAscendingAsync();

    Task<ObservableCollection<StudentListModel>> GetStudentsSortedBySurnameDescendingAsync();
}
