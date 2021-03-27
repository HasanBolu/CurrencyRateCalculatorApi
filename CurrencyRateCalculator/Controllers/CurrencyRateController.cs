using System.Net;
using System.Threading.Tasks;
using CurrencyRateCalculator.Models;
using CurrencyRateCalculator.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRateCalculator.Controllers
{
    public class CurrencyRateController : Controller
    {
        private readonly IRateCalculatorService _rateCalculatorService;
        
        public CurrencyRateController(IRateCalculatorService rateCalculatorService)
        {
            _rateCalculatorService = rateCalculatorService;
        }
        
        [Route("rate")]
        public async Task<IActionResult> ConversionRate([FromQuery]string currencyPair)
        {
            if (string.IsNullOrEmpty(currencyPair) || currencyPair.Trim().Length != 6)
            {
                return Json(ResponseModel.GetResponse((int)HttpStatusCode.BadRequest, "Currency pair text is invalid."));
            }

            var currency1 = currencyPair.Substring(0, 3);
            var currency2 = currencyPair.Substring(3, 3);

            var rate = await _rateCalculatorService.CalculateConversionRate(currency1, currency2);
            if (rate == -1)
            {
                return Json(ResponseModel.GetResponse((int)HttpStatusCode.BadRequest, "Currency pair text is invalid."));
            }

            return Json(ResponseModel.GetResponse((int)HttpStatusCode.OK,$"1 {currency1} => {rate} {currency2}"));
        }
        
    }
}