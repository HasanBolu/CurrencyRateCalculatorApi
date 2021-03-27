# CurrencyRateCalculatorApi

## Description
This is a .NET Core api that uses the contents of the XML file at https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml (which gets updated regularly), and exposes an HTTP endpoint to retrieve the rate for a specific currency pair. For example, a GET request to http://localhost:5001/rate?currencypair=GBPUSD returns the rate to convert GBP to USD. 

## Example Request
https://localhost:50001/rate?currencyPair=GBPUSD

### Example Response

```json
{
  "statusCode":200,
  "message":"1 GBP = 1.3780 USD"
}
```
