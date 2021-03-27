using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using CurrencyRateCalculator.Models;
using ExampleApi.Helpers;

namespace CurrencyRateCalculator.Services
{
    public class RateCalculatorService : IRateCalculatorService
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ICacheHelper _cacheHelper;
        
        private const string CURRENCY_XML_URL = "/stats/eurofxref/eurofxref-daily.xml";
        private const string CURRENCY_CACHE_KEY = "CurrencyList";
        
        public RateCalculatorService(IHttpClientHelper httpClientHelper, ICacheHelper cacheHelper)
        {
            _httpClientHelper = httpClientHelper;
            _cacheHelper = cacheHelper;
        }

        public async Task<double> CalculateConversionRate(string currency1, string currency2)
        {
            var currencyList = await GetCurrencyList();
            if (currencyList.Any(c => c.Name == currency1) && currencyList.Any(c => c.Name == currency2))
            {
                var firstCurrency = currencyList.FirstOrDefault(c => c.Name == currency1);
                var secondCurrency = currencyList.FirstOrDefault(c => c.Name == currency2);

                return secondCurrency.Rate / firstCurrency.Rate;
            }

            return -1;
        }


        private async Task<List<CurrencyModel>> GetCurrencyList()
        {
            string currencies;
            if (!_cacheHelper.TryGetValue(CURRENCY_CACHE_KEY, out currencies))
            {
                currencies = await _httpClientHelper.GetAsync(CURRENCY_XML_URL);
                _cacheHelper.InsertEntry(CURRENCY_CACHE_KEY, currencies, TimeSpan.FromHours(1));
            }
            
            var currencyXml = XDocument.Parse(currencies);

            return currencyXml.Descendants()
                .Where(x => x.Name.LocalName == "Cube" && x.Attribute("currency") != null).Select(x => new CurrencyModel
                {
                    Name = (string) x.Attribute("currency"),
                    Rate = (double) x.Attribute("rate")
                }).ToList();
        }
    }
}