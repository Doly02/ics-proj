using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class EnrolledModelMapper 
    : ModelMapperBase<EnrolledEntity, EnrolledSubjectsListModel, SubjectDetailModel>
{
    public override EnrolledSubjectsListModel MapToListModel(EnrolledEntity? entity)
    {
        if (entity?.Subject is null)
            return EnrolledSubjectsListModel.Empty;
        
        var mapper = new ActivityModelMapper();

        var activitiesListModel = mapper.MapToListModel(entity.Subject.Activities);
        var observableActivitiesListModel =
            new ObservableCollection<ActivityListModel>(activitiesListModel);
        
        return new EnrolledSubjectsListModel
        {
                
            Activities = observableActivitiesListModel,
            Abbreviation = entity.Subject.Abbreviation,
            Id = entity.Id,
            Name = entity.Subject.Name,
            StudentFullName = entity.Student.Name
        };
    }

    public override SubjectDetailModel MapToDetailModel(EnrolledEntity entity)
        => entity?.Subject is null
            ? SubjectDetailModel.Empty
            : new SubjectDetailModel
            {
                Id = entity.Subject.Id,
                Name = entity.Subject.Name,
                Abbreviation = entity.Subject.Abbreviation
            };
    
    public override EnrolledEntity MapToEntity(SubjectDetailModel model)
    {
        throw new NotImplementedException("Cannot map SubjectDetailModel to EnrolledEntity.");
    }
}
