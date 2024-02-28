using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.DAL.Entities
{
    public record StudentEntity
    {
        public required string Name { get; set; }

        public required string Surname { get; set; }
        public string? ImageUrl { get; set; }

        //public ICollection<SubjectEntity> Subjects { get; init; } = new List<SubjectEntity>();
        public ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();

        public ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();
        public required Guid Id { get; set; }
    }
}
