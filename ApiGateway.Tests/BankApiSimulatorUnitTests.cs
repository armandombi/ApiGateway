using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BankApiSimulator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentApi.Core.Models;
using PaymentApi.Core.Models.Enums;
using Xunit;

namespace ApiGateway.Tests
{
    public class BankApiSimulatorUnitTests
    {
        private readonly PaymentController _mockBankController;

        public BankApiSimulatorUnitTests()
        {
            _mockBankController = new PaymentController();
        }

        [Fact]
        public async Task WhenPaymentRequestIsValid_OkResponseShouldBeReturned()
        {
            var response = await _mockBankController.Process(new PaymentRequest());
            var result = response as OkObjectResult;
            var bankResponse = result.Value as BankResponse;

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<BankResponse>(result.Value);
            Assert.Equal(TransactionStatus.Complete, bankResponse.Status);
        }

        [Fact]
        public async Task WhenPaymentRequestIsInvalid_BadRequestResponseShouldBeReturned()
        {
            var response = await _mockBankController.Process(new PaymentRequest{CardHolderName = "FailedRequest" });
            var result = response as OkObjectResult;
            var bankResponse = result.Value as BankResponse;

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<BankResponse>(result.Value);
            Assert.Equal(TransactionStatus.Failed, bankResponse.Status);
        }
    }
}
