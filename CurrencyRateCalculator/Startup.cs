using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyRateCalculator.Middleware;
using CurrencyRateCalculator.Models;
using CurrencyRateCalculator.Services;
using ExampleApi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurrencyRateCalculator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient("ConversionRateApi", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("ConversionRateApiUrl").Value);
            });
            services.AddTransient<IRateCalculatorService, RateCalculatorService>();
            services.AddSingleton<IHttpClientHelper, HttpClientHelper>(s =>
                new HttpClientHelper(s.GetService<IHttpClientFactory>(), "ConversionRateApi")
            );
            
            services.AddMemoryCache();
            services.AddTransient<ICacheHelper, CacheHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
        }
    }
}