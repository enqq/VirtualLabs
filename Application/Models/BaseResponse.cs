using System;
namespace Application.Models
{
    public class BaseResponse
    {
       public string? Message { get; set; }
       public bool Status { get; set; }

       public BaseResponse(bool status)
        {
            this.Message = string.Empty;
            this.Status = status;
        }

        public BaseResponse(string message, bool status)
        {
            this.Message = message;
            this.Status = status;
        }
    }
}

