using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class StudentModelMapper : ModelMapperBase<StudentEntity, StudentListModel, StudentDetailModel>
{
    public override StudentListModel MapToListModel(StudentEntity? entity)
        => entity is null
            ? StudentListModel.Empty
            : new StudentListModel { Id = entity.Id, Name = entity.Name, Surname = entity.Surname };

    public override StudentDetailModel MapToDetailModel(StudentEntity? entity)
        => entity is null
            ? StudentDetailModel.Empty
            : new StudentDetailModel
            {
                Id = entity.Id, Name = entity.Name, Surname = entity.Surname, ImageUrl = entity.ImageUrl
            };

    public override StudentEntity MapToEntity(StudentDetailModel model)
        => new() { Id = model.Id, Name = model.Name, Surname = model.Surname, ImageUrl = model.ImageUrl };
}