using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CurrencyRateCalculator.Models;
using Microsoft.AspNetCore.Http;

namespace CurrencyRateCalculator.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var response = context.Response;
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                
                var result = JsonSerializer.Serialize(ResponseModel.GetResponse((int) HttpStatusCode.InternalServerError, e?.Message));
                
                await response.WriteAsync(result);
            }
        }
    }
}