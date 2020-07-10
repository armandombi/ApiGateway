using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentApi.Core.Models;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using PaymentApi.Core.Models.Enums;

namespace BankApiSimulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost()]
        [ProducesResponseType(typeof(BankResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Process([FromBody][Required] PaymentRequest request)
        {
            try
            {
                var bankResponse = request.CardHolderName == "FailedRequest"
                    ? new BankResponse {Id = Guid.NewGuid(), Status = TransactionStatus.Failed}
                    : new BankResponse {Id = Guid.NewGuid(), Status = TransactionStatus.Complete};

                return Ok(bankResponse);
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
