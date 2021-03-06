﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using MoneyChangrer.Martin.Sun.Models;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace MoneyChanger.Martin.Sun
{
    public interface IMoneyChangerService
    {
        JsonResult GetExchangeRate(string ExchangeCurrency);
        JsonResult GetExchangeRates();
    }
    public class MoneyChangrerService : IMoneyChangerService
    {
        private readonly ILogger<MoneyChangrerService> _logger;
        IConfiguration _configuration;
        public List<ExchangeCurrency> currencyList { get; set; }
        public MoneyChangrerService(IConfiguration configuration, ILogger<MoneyChangrerService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            try
            {
                var DBFile = _configuration["DBFileName"];
                XmlSerializer ser = new XmlSerializer(typeof(List<ExchangeCurrency>));
                currencyList = new List<ExchangeCurrency>();
                using (FileStream stream = File.OpenRead(DBFile))
                {
                    currencyList = (List<ExchangeCurrency>)ser.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message+":"+ex.StackTrace);

            }

        }
        public JsonResult GetExchangeRate(string ExchangeCurrency)
        {
            JsonResult result = new JsonResult();
            try
            {
                if (currencyList.Where(x => x.Name == ExchangeCurrency).Any())
                {
                     result.Message = string.Format("Excahnge Rate {0} ",ExchangeCurrency);
                    result.Data = currencyList.Where(x => x.Name == ExchangeCurrency).First();
                    result.HttpStatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    result.Message = "Specified Currecny Not Found";
                    result.HttpStatusCode = StatusCodes.Status404NotFound;
                }




            }
            catch (Exception ex)
            {
                result.HttpStatusCode = StatusCodes.Status500InternalServerError;
                result.Message = ex.Message;
            }
           
            return result;
        }

        public JsonResult GetExchangeRates()
        {
            JsonResult result = new JsonResult();
            try
            {
                if (currencyList.Any())
                {
                    result.Message = string.Format("Excahnge Rate on: {0} ", DateTime.Now.ToShortTimeString());
                    result.Data = currencyList;
                    result.HttpStatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    result.Message = "No Currecny in the System";
                    result.HttpStatusCode = StatusCodes.Status404NotFound;
                }




            }
            catch (Exception ex)
            {
                result.HttpStatusCode = StatusCodes.Status500InternalServerError;
                result.Message = ex.Message;
            }

            return result;
        }
    } 
}
