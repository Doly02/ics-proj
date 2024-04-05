using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class ActivityModelMapper : 
    ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>,IActivityModelIMapper
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel
            {
                Id = entity.Id, 
                ActivityName = entity.Name,
                Activity = [MapToDetailModel(entity)],
                SubjectName = entity.Subject.Name
            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id, 
                Name = entity.Name, 
                ActivityType = entity.ActivityType, 
                Description = entity.Description,
                End = entity.End,
                Place = entity.Place,
                Start = entity.Start,
                SubjectName = entity.Subject.Name
            };
    
    public ActivityListModel MapToExistingDetailModel(ActivityDetailModel detailModel)
        => new()
            {
                Id = detailModel.Id,
                Activity = [detailModel],
                ActivityName = detailModel.Name,
                SubjectName = detailModel.SubjectName
            };
    
    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => throw new NotImplementedException("This method is unsupported. Use the other overload.");

    public  ActivityEntity MapToEntity(ActivityDetailModel model, Guid subjectId)
        => new() { Id = model.Id, 
            Name = model.Name, 
            ActivityType = model.ActivityType, 
            Description = model.Description,
            End = model.End,
            Place = model.Place,
            Start = model.Start,
            Subject = null!,
            Evaluations = Array.Empty<EvaluationEntity>(),
            SubjectId = subjectId 
        };
}
