﻿using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Mappers
{
    public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
    {
        public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
        {
            existingEntity.Id = newEntity.Id;
            existingEntity.Start = newEntity.Start;
            existingEntity.End = newEntity.End;
            existingEntity.Place = newEntity.Place;
            existingEntity.ActivityType = newEntity.ActivityType;
            existingEntity.Description = newEntity.Description;
        }
    }
}