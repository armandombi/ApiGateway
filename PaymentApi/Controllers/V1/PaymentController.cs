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
using Microsoft.AspNetCore.Authorization;
using PaymentApi.Core.Models.DTO;

namespace PaymentApi.Controllers.V1
{
    /// <summary>
    /// Controller to handle all operations related to payments
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Constructor to initialize all the services and dependencies used in the payment controller
        /// </summary>
        /// <param name="paymentService">The service to handles all operations related to payments</param>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        /// <summary>
        /// Process a payment
        /// </summary>
        /// <param name="id">The payment unique identifier</param>
        /// <param name="request">The payment request containing the information to be processed</param>
        /// <response code="201">Payment created</response>
        /// <response code="400">Payment has missing/invalid values</response>
        /// <response code="500">There is an issue processing the payment</response>
        /// <returns>A created response if the process is successful or a bad request response if the process fails</returns>
        [HttpPost("{id:guid}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Retrieve an existing payment
        /// </summary>
        /// <param name="id">The payment unique identifier</param>
        /// <response code="200">The payment details</response>
        /// <response code="400">The request has missing/invalid values</response>
        /// <response code="404">The payment with the id specified does not exist</response>
        /// <response code="500">There is an issue retrieving the payment</response>
        /// <returns>The payment details or a not found response</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PaymentDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var paymentDocument = await _paymentService.GetPayment(id);
                if (paymentDocument == null)
                    return NotFound($"Payment id: {id} was not found");

                var paymentDetails = new PaymentDto(paymentDocument);
                return Ok(paymentDetails);
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
