using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class ValuesLogs : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? Value { get; set; }

        
        public MeasurementLogs Parent { get; set; }

        public ValuesLogs() { }
        public ValuesLogs(string name, string value, MeasurementLogs parent)
        {
            (Name, Value, Parent) = (name, value, parent);
        }

    }
}

