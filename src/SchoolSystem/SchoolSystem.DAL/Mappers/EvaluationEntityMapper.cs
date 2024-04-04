using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Mappers;

public class EvaluationEntityMapper : IEntityMapper<EvaluationEntity>
{
    public void MapToExistingEntity(EvaluationEntity existingEntity, EvaluationEntity newEntity)
    {
        existingEntity.StudentId = newEntity.StudentId;
        existingEntity.ActivityId = newEntity.ActivityId;
        existingEntity.Score = newEntity.Score;
        existingEntity.Note = newEntity.Note;
    }
}
