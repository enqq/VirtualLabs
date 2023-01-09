using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class Position: BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public Lab? Lab { get; set; }
        public List<ValuesLogs>? ValuesLogs { get; set; }

        public Position(string name, string? description)
        {
            (Name, Description) = (name, description);
        }
    }
}

