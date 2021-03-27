using System.Reflection.Metadata.Ecma335;

namespace CurrencyRateCalculator.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public static ResponseModel GetResponse(int statusCode, string message)
        {
            return new ResponseModel()
            {
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}