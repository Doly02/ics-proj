﻿using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Mappers
{
    public class SubjectEntityMapper : IEntityMapper<SubjectEntity>
    {
        public void MapToExistingEntity(SubjectEntity existingEntity, SubjectEntity newEntity)
        {
            existingEntity.Name = newEntity.Name;
            existingEntity.Abbreviation = newEntity.Abbreviation;
        }
    }
}
