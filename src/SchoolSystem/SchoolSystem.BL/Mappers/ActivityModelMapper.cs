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
                Name = entity.Name,
                // StudentFullName = 
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
                SubjectName = null!
            };
    
    public ActivityListModel MapToListModel(ActivityDetailModel detailModel)
        => new()
            {
                Id = detailModel.Id, 
                Name = detailModel.Name,
                // StudentFullName = 
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
