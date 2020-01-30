using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyChangrer.Martin.Sun.Interface;
using MoneyChangrer.Martin.Sun.Models;
namespace Coding.Challenge.Martin.Sun
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
              .AddConfiguration(configuration)          
                .Build();
           
        }
    
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITradingCurrency, ExchangeCurrency>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        // Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        { 
            app.UseMvc();
        }
    
    }
}
