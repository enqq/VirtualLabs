using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class MeasurementLogs : BaseModel
    {
        
        public string Name { get; set; }
        public List<ValuesLogs?> Values { get; set; }
        public User? Teacher { get; set; }
        //public User CreatedBy { get; set; }
        public List<User>? SharedFor { get; set; }
        public List<UserGroup?> SharedForGroups { get; set; }

    }
}

