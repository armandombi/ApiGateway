using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentApi.Controllers.V1;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;
using PaymentApi.Core.Models.DTO;
using Xunit;

namespace ApiGateway.Tests
{
    public class PaymentApiUnitTests
    {
        public PaymentApiUnitTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _mockController = new PaymentController(_mockPaymentService.Object);
            _mockController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        private readonly PaymentController _mockController;
        private readonly Mock<IPaymentService> _mockPaymentService;

        [Fact]
        public async Task WhenPaymentDocumentIsFound_PaymentDetailsShouldBeReturned()
        {
            _mockPaymentService
                .Setup(x => x.GetPayment(It.IsAny<Guid>()))
                .ReturnsAsync(new PaymentDocument());

            var response = await _mockController.Get(Guid.Empty);
            var result = response as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<PaymentDto>(result.Value);
        }

        [Fact]
        public async Task WhenPaymentDocumentIsNotFound_PaymentDetailsShouldBeReturned()
        {
            _mockPaymentService
                .Setup(x => x.GetPayment(It.IsAny<Guid>()))
                .ReturnsAsync((PaymentDocument) null);

            var response = await _mockController.Get(Guid.Empty);
            var result = response as NotFoundObjectResult;

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task WhenPaymentRequestIsInvalid_BadRequestResponseShouldBeReturned()
        {
            _mockPaymentService
                .Setup(x => x.ProcessPayment(It.IsAny<Guid>(), It.IsAny<PaymentRequest>()))
                .ReturnsAsync(false);

            var response = await _mockController.Process(Guid.Empty, new PaymentRequest());
            var result = response as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task WhenPaymentRequestIsValid_CreatedResponseShouldBeReturned()
        {
            _mockPaymentService
                .Setup(x => x.ProcessPayment(It.IsAny<Guid>(), It.IsAny<PaymentRequest>()))
                .ReturnsAsync(true);

            var response = await _mockController.Process(Guid.Empty, new PaymentRequest());
            var result = response as CreatedResult;

            Assert.IsType<CreatedResult>(result);
        }
    }
}