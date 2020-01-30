using MoneyChangrer.Design.Martin.Sun.Interface;

namespace MoneyChangrer.Design.Martin.Sun
{
    public class ExchangeCurrency:Currency, ITradingCurrency
    {
        private decimal exchangeRate { get; set; }
        private decimal exchangeAmount { get; set; }
        public int getID()
        { return id; }
        public void setID(int Id)
        { id=Id; }
        public string getName()
        { return name; }
        public void setName(string Name)
        { name=Name; }

        public decimal getExchangeRate(Currency targetCurrency)
        {
            return exchangeRate;
        }
        public decimal getExchangedAmount(Currency targetCurrency)
        {
            return exchangeAmount;
        }
    }
}
