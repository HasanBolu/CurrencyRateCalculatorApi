using System.Threading.Tasks;

namespace CurrencyRateCalculator.Services
{
    public interface IRateCalculatorService
    {
        public Task<double> CalculateConversionRate(string currency1, string currency2);
    }
}