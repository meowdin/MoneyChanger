using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Coding.Challenge.Martin.Sun.Test
{
    [TestClass]
    public class MoneyChangrerControllerTest
    {
        MoneyChangrerController _controller;
        IConfiguration _configuration;
        IDTLService _dTLService;
         [TestInitialize]
        public void Initialize()
        {
            _configuration = Mock.Of<IConfiguration>();
            _dTLService = new MoneyChangrerService(_configuration);
               _controller = new MoneyChangrerController(_configuration,_dTLService);
        }
        [TestMethod]
        public void SyncOutlets_ShouldReturn404()
        {
            IActionResult Result = _controller.ParseModel("FailedType",1);
            Assert.IsInstanceOfType(Result, typeof(ObjectResult));
            Assert.IsTrue(((ObjectResult)Result).Value.ToString().Contains("template does not exist!"));
            Assert.IsTrue(((ObjectResult)Result).StatusCode == StatusCodes.Status404NotFound);
        }
        [TestMethod]
        public void SyncOutlets_ShouldReturnNoObject()
        {
            
            IActionResult Result = _controller.ParseModel("Car", 3);
            Assert.IsInstanceOfType(Result, typeof(ObjectResult));
            Assert.IsTrue(((ObjectResult)Result).Value.ToString().Contains("not exist"));
            Assert.IsTrue(((ObjectResult)Result).StatusCode == StatusCodes.Status404NotFound);
        }
    }
}
