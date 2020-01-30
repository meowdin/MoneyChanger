﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using MoneyChangrer.Martin.Sun.Models;
using System.Xml.Serialization;

namespace MoneyChanger.Martin.Sun
{
    public interface IMoneyChangerService
    {
        JsonResult GetExchangeRate(string ExchangeCurrency);
    }
    public class MoneyChangrerService : IMoneyChangerService
    {
        IConfiguration _configuration;
        public List<ExchangeCurrency> currencyList { get; set; }
        public MoneyChangrerService(IConfiguration configuration)
        {
            _configuration = configuration;
            var DBFile = _configuration["DBFileName"];
            XmlSerializer ser = new XmlSerializer(typeof(List<ExchangeCurrency>));
            using (FileStream stream = File.OpenRead(DBFile))
            {
                currencyList = (List<ExchangeCurrency>)ser.Deserialize(stream);
            }

        }
        public JsonResult GetExchangeRate(string ExchangeCurrency)
        {
            JsonResult result = new JsonResult();
            try
            {
                if (currencyList.Where(x => x.Name == ExchangeCurrency).Any())
                {
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
       
    } 
}