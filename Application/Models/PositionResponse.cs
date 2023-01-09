using System;
namespace Application.Models
{
    public class PositionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class PositionsListResponse: PositionResponse
    {
        public List<ValueLogsPositionResponse>? ValuesLogs { get; set; }
    }
}

