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

    }
}

