using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.Models
{
    public class LabRequest
    {       
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }   
    }

    public class LabUpdateRequest: LabRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class LabPositionRequest
    {
        [Required]
        public int Lab { get; set; }
        [Required]
        public int Position { get; set; }
    }
}

