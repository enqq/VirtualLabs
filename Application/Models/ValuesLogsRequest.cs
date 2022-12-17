using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ValuesLogsRequest
    {            
        [Required]
        public string Value { get; set; }
        public string? Type { get; set; }
        public string? Format { get; set; }
    }

    public class ValuesLogsCreateRequest : ValuesLogsRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int LogsID { get; set; }
    }

    public class ValuesLogsUpdateRequest : ValuesLogsRequest
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int LogID { get; set; }

        public string? Name { get; set; }
        
    }
}
