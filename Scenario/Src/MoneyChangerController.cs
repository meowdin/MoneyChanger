using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace MoneyChanger.Martin.Sun
{
  
    [Route("api/v1/MoneyChangrer")]
    [ApiController]
    public class MoneyChangrerController : Controller
    {      
        private readonly IConfiguration _config;
        private readonly IMoneyChangerService _moneyChangerService;
        public MoneyChangrerController(IConfiguration configuration, IMoneyChangerService moneyChangerService)
        {           
            _config = configuration;
            _moneyChangerService = moneyChangerService;
            
        }
        /// <summary>
        /// search for the reate in DB and return the result
        /// </summary>       
        /// <returns>HTTP Response</returns>
        /// <remarks> </remarks>
        [HttpGet("getrate")]
        public IActionResult GetRates(string ExchangeCurrency)
        {           
         
            var rate = _moneyChangerService.GetExchangeRate(ExchangeCurrency);
            return Ok(rate); 
        }
        /// <summary>
        /// dashboard
        /// </summary>       
        /// <returns>HTTP Response</returns>
        /// <remarks> </remarks>
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {

            var rates = _moneyChangerService.GetExchangeRates();
            return Ok(rates);
        }

    }
}
