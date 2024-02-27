using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.DAL.Entities
{
    public record EvaluationEntity : IEntity
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string? Note { get; set; }
        public required StudentEntity Student { get; set; }



    }
}
