using SchoolSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.DAL.Mappers
{
    public class EnrolledEntityMapper 
    {
        public void MapToExistingEntity(EnrolledEntity existingEntity, EnrolledEntity newEntity)
        {
            existingEntity.StudentId = newEntity.StudentId;
            existingEntity.SubjectId = newEntity.SubjectId;

        }

    }
}
