using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class StudentModelMapper : ModelMapperBase<StudentEntity, StudentListModel, StudentDetailModel>
{
    public override StudentListModel MapToListModel(StudentEntity? entity)
        => entity is null
            ? StudentListModel.Empty
            : new StudentListModel { Id = entity.Id, Name = entity.Name, Surname = entity.Surname };
    
    
    private readonly EnrolledModelMapper _enrolledModelMapper = new EnrolledModelMapper();
    
    
    public override StudentDetailModel MapToDetailModel(StudentEntity? entity)
    {
        if (entity is null)
        {
            return StudentDetailModel.Empty;
        }

        var enrolledSubjectsListModels = entity.Enrolleds?
            .Select(enrolleds => _enrolledModelMapper.MapToListModel(enrolleds))
            .ToList();

        return new StudentDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Surname = entity.Surname,
            ImageUrl = entity.ImageUrl,
            EnrolledSubjects = new ObservableCollection<EnrolledSubjectsListModel>(enrolledSubjectsListModels ?? new List<EnrolledSubjectsListModel>())
        };
    }
    
    public override StudentEntity MapToEntity(StudentDetailModel model)
        => new() { Id = model.Id, Name = model.Name, Surname = model.Surname, ImageUrl = model.ImageUrl };
}