using MoneyChangrer.Martin.Sun.Models;

namespace MoneyChangrer.Martin.Sun.Interface
{
    public interface ITradingCurrency
    {
        decimal getExchangeRate(Currency targetCurrency);
        decimal getExchangedAmount(Currency targetCurrency);
    }
}
