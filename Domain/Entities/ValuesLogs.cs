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
        public string? Type { get; set; }
        public string? Format { get; set; }
        
        public MeasurementLogs Parent { get; set; }

        public ValuesLogs() { }
        public ValuesLogs(string name, string value, string type, string format, MeasurementLogs parent)
        {
            (Name, Value, Type, Format, Parent) = (name, value, type, format, parent);
        }

    }
}

