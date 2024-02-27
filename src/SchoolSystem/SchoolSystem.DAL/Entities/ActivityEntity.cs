using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.DAL.Entities;

public record ActivityEntity : IEntity
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int ActivityType { get; set; }
    public string? Description { get; set;  }

    public required Guid SubjectId { get; set; }
    public ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();

    public Guid Id { get; set; }
}

