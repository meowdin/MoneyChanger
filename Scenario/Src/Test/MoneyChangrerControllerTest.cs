using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoneyChanger.Martin.Sun.Test
{
    [TestClass]
    public class MoneyChangrerControllerTest
    {
       
        private Mock<IConfiguration> _configuration;
        IMoneyChangerService _moneyChangerService;
        private Mock<ILogger<MoneyChangrerService>> _logger;
        [TestInitialize]
        public void Initialize()
        {
            _configuration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<MoneyChangrerService>>();
            _moneyChangerService = new MoneyChangrerService(_configuration.Object, _logger.Object);
             
        }
        [TestMethod]
        public void getrate_ShouldReturn404()
        {
            JsonResult Result = _moneyChangerService.GetExchangeRate("NOTEXIST");           
            Assert.IsTrue(Result.HttpStatusCode == StatusCodes.Status404NotFound);
        }
        [TestMethod]
        public void getrates_ShouldReturn404()
        {
            JsonResult Result = _moneyChangerService.GetExchangeRates();
            Assert.IsTrue(Result.HttpStatusCode == StatusCodes.Status404NotFound);
        }
    }
}
