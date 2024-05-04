using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class EnrolledModelMapper 
    : ModelMapperBase<EnrolledEntity, EnrolledSubjectsListModel, EnrolledDetailModel>
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
            Id = entity.Id,
            SubjectId = entity.SubjectId,
            SubjectName = entity.Subject.Name,
            StudentFullName = entity.Student.Name
        };
    }

    public EnrolledSubjectsListModel MapToListModel(EnrolledSubjectsListModel detailModel)
    {
        var activitiesListModels = new ObservableCollection<ActivityListModel>();

        foreach (var activityDetail in detailModel.Activities)
        {
            var newActivityList = new ActivityListModel
            {
                ActivityName = activityDetail.ActivityName,
                SubjectName = activityDetail.SubjectName,
                Activity = new ObservableCollection<ActivityDetailModel>()
            };

            activitiesListModels.Add(newActivityList);
        }

        return new EnrolledSubjectsListModel
        {
            Id = detailModel.Id,
            SubjectId = detailModel.SubjectId,
            SubjectName = detailModel.SubjectName,
            StudentFullName = detailModel.StudentFullName,
            Activities = activitiesListModels
        };
    }

    public void MapToExistingListModel(EnrolledSubjectsListModel existingListModel,
       SubjectListModel subject)
    {
        existingListModel.SubjectId = subject.Id;
        existingListModel.SubjectName = subject.Name;
        existingListModel.Activities = subject.Activities;
    }

    public override EnrolledDetailModel MapToDetailModel(EnrolledEntity entity)
        => entity?.Subject is null
            ? EnrolledDetailModel.Empty
            : new EnrolledDetailModel
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                SubjectId = entity.SubjectId,
                SubjectName = entity.Subject.Name

            };

    public override EnrolledEntity MapToEntity(EnrolledDetailModel model)
    {
        throw new NotImplementedException("Cannot map EnrolledDeatilModel to EnrolledEntity.");
    }

    public EnrolledEntity MapToEntity(EnrolledDetailModel model, Guid studentId)
        => new()
        {
            Id = model.Id,
            StudentId = studentId,
            SubjectId = model.SubjectId,
            Student = null!,
            Subject = null!
        };

    public EnrolledEntity MapToEntity(EnrolledSubjectsListModel model, Guid studentId)
        => new EnrolledEntity
        {
            Id = model.Id,
            StudentId = studentId, // Předpokládáme, že studentId je známý
            SubjectId = model.SubjectId,
            // Další vlastnosti modelu závisí na tom, co všechno EnrolledListModel obsahuje
            Student = null!, // Explicitně nastavujeme null
            Subject = null! // Explicitně nastavujeme null
        };
}
