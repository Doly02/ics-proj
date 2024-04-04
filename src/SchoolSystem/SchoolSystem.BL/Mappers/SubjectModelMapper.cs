using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class SubjectModelMapper : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailModel>
{
    public override SubjectListModel MapToListModel(SubjectEntity? entity)
        => entity is null
            ? SubjectListModel.Empty
            : new SubjectListModel { Id = entity.Id, Name = entity.Name, Abbreviation = entity.Abbreviation };

    public override SubjectDetailModel MapToDetailModel(SubjectEntity? entity)
        => entity is null
            ? SubjectDetailModel.Empty
            : new SubjectDetailModel
            {
                Id = entity.Id, Name = entity.Name, Abbreviation = entity.Abbreviation
            };

    public override SubjectEntity MapToEntity(SubjectDetailModel model)
        => new() { Id = model.Id, Name = model.Name, Abbreviation = model.Abbreviation };
}