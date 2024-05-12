using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public interface IActivityModelIMapper
    : IModelMapper<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    ActivityEntity MapToEntity(ActivityDetailModel model, Guid subjectId); // for subject guid

    ActivityListModel MapToListModelWithEval(ActivityEntity? entity, int score); // evaluation displaying for students
}
