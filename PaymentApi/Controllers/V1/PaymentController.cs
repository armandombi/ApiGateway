using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;

namespace PaymentApi.Controllers.V1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Process([FromRoute] Guid id, [FromBody][Required] PaymentRequest request)
        {
            try
            {
                if (!await _paymentService.ProcessPayment(id, request))
                    return BadRequest($"Failed to process payment {id}");

                var path = HttpContext.Request.Path.Value;
                return Created($"{path}", null);
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
