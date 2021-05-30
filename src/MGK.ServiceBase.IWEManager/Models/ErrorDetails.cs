using Newtonsoft.Json;

namespace MGK.ServiceBase.IWEManager.Models
{
    public sealed class ErrorDetails
    {
        public ErrorDetails(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
		}

        public int StatusCode { get; }

        public string Message { get; }

        public string Details { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
