using System;
namespace Application.Models
{
    public class ValueLogsResponse
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}

