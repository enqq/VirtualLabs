using System;
namespace Application.Models
{
    public class ValueLogsResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string? Type { get; set; }
        public string? Format { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}

