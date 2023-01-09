using System;
namespace Application.Models
{
    public class LabRespone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<PositionResponse>? Positions { get; set; }
    }
}

