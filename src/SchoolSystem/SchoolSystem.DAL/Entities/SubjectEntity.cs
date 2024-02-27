using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.DAL.Entities
{
    public record SubjectEntity : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        
        /*private string? _abbreviation;

        public string? Abbreviation
        {
            get => _abbreviation;
            set => _abbreviation = value?.ToUpper();
        }*/
    }
}
