using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MoneyChanger.Martin.Sun
{
  
    [Route("api/MoneyChangrer")]
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
        [HttpGet("GetRates")]
        public IActionResult GetRates(string ExchangeCurrency)
        {           
         
            var rate = _moneyChangerService.GetExchangeRate(ExchangeCurrency);
            return Ok(rate); 
        }
        
      
    }
}
