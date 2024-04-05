using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class SubjectModelMapper : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailModel>
{
    public override SubjectListModel MapToListModel(SubjectEntity? entity)
    {
        if (entity is null) return SubjectListModel.Empty;

        var map = new ActivityModelMapper();
        var activitiesListModel = map.MapToListModel(entity.Activities);
        var observableActivitiesListModel = new ObservableCollection<ActivityListModel>(activitiesListModel);

        return new SubjectListModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Abbreviation = entity.Abbreviation,
            Activities = observableActivitiesListModel
        };
    }

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