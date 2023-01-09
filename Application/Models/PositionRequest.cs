using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class PositionRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? LabId { get; set; }
    }
    public class PositionUpdateRequest: PositionRequest
    {
        [Required]
        public int Id { get; set; }

    }
}

