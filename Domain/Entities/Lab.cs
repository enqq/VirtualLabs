using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class Lab: BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Position>? Positions { get; set; }
        public List<MeasurementLogs>? MeasurementLogs { get; set; }

        public Lab(string name, string? description)
        {
            (Name, Description) = (name, description);
        }
    }
}

