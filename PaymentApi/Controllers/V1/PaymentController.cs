using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;

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
            return Ok();
        }
        
    }
}
