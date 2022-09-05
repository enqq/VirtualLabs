using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class MeasurementLogsRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int TeacherID { get; set; }       
        public List<int>? SharedFor { get; set; }
        public List<int>? SharedForGroup { get; set; }
    }

    public class MeasurementLogsCreateRequest : MeasurementLogsRequest
    {
        public List<ValuesLogsCreateRequest>? Values { get; set; }
    }

    public class MeasurementLogsUpdateRequest : MeasurementLogsRequest
    {
        [Required]
        public int ID { get; set; }
        public List<ValuesLogsUpdateRequest>? Values { get; set; }
    }

    public class MeasurmentLogsSharedRequest
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int ForeginID { get; set; }
    }


}

