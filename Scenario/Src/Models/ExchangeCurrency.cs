
using System;

namespace MoneyChangrer.Martin.Sun.Models
{
    public class ExchangeCurrency
    {
        public int ID { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Name { get; set; }
        public int? DecimalDigit { get; set; }

        public decimal FormattedExchangeRate
        {
            get
            {
                return Math.Round(ExchangeRate ?? 0, DecimalDigit ?? 0, MidpointRounding.AwayFromZero);
            }
                
        }
    }
}
