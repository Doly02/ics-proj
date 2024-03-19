﻿using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Mappers
{
    public class StudentEntityMapper : IEntityMapper<StudentEntity>
    {
        public void MapToExistingEntity(StudentEntity existingEntity, StudentEntity newEntity)
        {
            existingEntity.Name = newEntity.Name;
            existingEntity.Surname = newEntity.Surname;
            existingEntity.ImageUrl = newEntity.ImageUrl;
        }
    }
}
