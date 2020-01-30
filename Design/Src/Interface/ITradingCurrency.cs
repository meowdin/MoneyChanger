namespace MoneyChangrer.Design.Martin.Sun.Interface
{
    public interface ITradingCurrency
    {
        decimal getExchangeRate(Currency targetCurrency);
        decimal getExchangedAmount(Currency targetCurrency);
    }
}
