using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ValuesLogsRequest
    {
        [Required]
        public string Name { get; set; }
        public string? Value { get; set; }
    }

    public class ValuesLogsUpdateRequest : ValuesLogsRequest
    {
        [Required]
        public int ID { get; set; }
    }
}
