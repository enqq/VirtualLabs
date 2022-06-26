using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class BaseModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; }
    }
}

