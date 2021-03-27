using System.Reflection.Metadata.Ecma335;

namespace CurrencyRateCalculator.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

        public static ResponseModel GetResponse(int statusCode, string message, object data = null)
        {
            return new ResponseModel()
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }
    }
}