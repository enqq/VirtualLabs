using System;
using Domain.Entities;

namespace Application.Models
{
    public class MeasurementLogsResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ValueLogsResponse?> Values { get; set; }
        //public User? Teacher { get; set; }
        //public User CreatedBy { get; set; }
        //public List<User>? SharedFor { get; set; }
    }
}

