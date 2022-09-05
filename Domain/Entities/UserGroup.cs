using System;
using Domain.Common;
using Domain.Entities;

namespace Domain.Entities
{
    public class UserGroup: BaseModel
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<MeasurementLogs> MeasurementLogs { get; set; }

    }
}

