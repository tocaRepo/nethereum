using Microsoft.VisualStudio.TestTools.UnitTesting;
using NethereumApi.Controllers;
using NethereumApi.Services;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NethereumApiTests
{
    [TestClass]
    public class NethereumControllerTests
    {

        [TestMethod]
        public void Constructor_nethereumService_notFound_ThrowsArgumentNullException()
        {
            // Arrange
            INethereumService nethereumService = null;
            var logger = new Mock<ILogger<NethereumController>>();
            // Act / Assert
            Assert.ThrowsException<ArgumentNullException>(() => new NethereumController(logger.Object, nethereumService));
        }
        [TestMethod]
        public void Constructor_logger_notFound_ThrowsArgumentNullException()
        {
            // Arrange
            var nethereumService = new Mock<INethereumService>();
            ILogger<NethereumController> logger = null;
            // Act / Assert
            Assert.ThrowsException<ArgumentNullException>(() => new NethereumController(logger, nethereumService.Object));
        }

        [TestMethod]
        public async Task GetEthBalanceByWalletId_NullString_ReturnBadRequest()
        {
            // Arrange
            var nethereumService = new Mock<INethereumService>();
            var logger = new Mock<ILogger<NethereumController>>();
            nethereumService.Setup(rs => rs.GetEthBalanceByWalletIdAsync(It.IsAny<string>())).ReturnsAsync(150);
            var controller = new NethereumController(logger.Object, nethereumService.Object);
            string wallet = null;
            // Act 
            IActionResult result = await controller.GetEthBalanceByWalletId(wallet);
            // Assert
            var badRequestObjectResult = result as BadRequestResult;
            Assert.IsNotNull(badRequestObjectResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);
            nethereumService.Verify(mock => mock.GetEthBalanceByWalletIdAsync(wallet), Times.Never);

        }

        [TestMethod]
        public async Task GetEthBalanceByWalletId_ValidWallet_Return200()
        {
            // Arrange
            var nethereumService = new Mock<INethereumService>();
            var logger = new Mock<ILogger<NethereumController>>();
            nethereumService.Setup(rs => rs.GetEthBalanceByWalletIdAsync(It.IsAny<string>())).ReturnsAsync(150);
            var controller = new NethereumController(logger.Object, nethereumService.Object);
            string wallet = "0x0x0x0x0x0x0x0x0xxxx000xxx'";
            // Act 
            IActionResult result = await controller.GetEthBalanceByWalletId(wallet);
            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);
            nethereumService.Verify(mock => mock.GetEthBalanceByWalletIdAsync(wallet), Times.Once);
        }

        [TestMethod]
        public async Task GetEthBalanceByWalletId_ValidWallet_Exception_Return500()
        {
            // Arrange
            var nethereumService = new Mock<INethereumService>();
            var logger = new Mock<ILogger<NethereumController>>();
            nethereumService.Setup(rs => rs.GetEthBalanceByWalletIdAsync(It.IsAny<string>())).ThrowsAsync(new Exception("fake ex"));
            var controller = new NethereumController(logger.Object, nethereumService.Object);
            string wallet = "0x0x0x0x0x0x0x0x0xxxx000xxx'";
            // Act 
            IActionResult result = await controller.GetEthBalanceByWalletId(wallet);
            // Assert
            var statusCodeResult = result as StatusCodeResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            nethereumService.Verify(mock => mock.GetEthBalanceByWalletIdAsync(wallet), Times.Once);
        }

    }
}