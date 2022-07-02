using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.API.Commands;
using POC.API.Model;
using PaymentControllerLib = POC.API.Lib.Controllers.PaymentController;
using PaymentLib = POC.API.Lib.Model.Payment;

namespace POC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private IMediator _mediator;
        private readonly PaymentControllerLib paymentController;

        public PaymentController(IMediator mediator, ILogger<PaymentController> logger)
        {
            _logger = logger;
            _mediator = mediator;
            paymentController = new PaymentControllerLib();
        }

        /// <summary>
        /// Create a payment information.
        /// </summary>
        /// <response code="200">Payment is created</response>
        /// <response code="500">Bad request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Payment payment)
        {
            _logger.LogDebug($"Article created {payment}");
            return await _mediator.Send(new PostPayment(payment));
            //await paymentController.Post(payment);
        }

        // /// <summary>
        // /// Get all payment information.
        // /// </summary>
        // /// <response code="200">Payments are returned</response>
        // [HttpGet]
        // [ProducesResponseType(typeof(List<Payment>),StatusCodes.Status200OK)]
        // public async Task<IActionResult> Get()
        // {
        //     return await _mediator.Send(new GetPayments());
        // }
        
        // /// <summary>
        // /// Get specific payment information.
        // /// </summary>
        // /// <response code="200">Payment is returned</response>
        // [HttpGet("Name")]
        // [ProducesResponseType(typeof(Payment),StatusCodes.Status200OK)]
        // public async Task<IActionResult> Get(string name)
        // {
        //     return await _mediator.Send(new GetPayment(name));
        // }
        
        // /// <summary>
        // /// Delete specific payment information.
        // /// </summary>
        // /// <response code="200">Payment is deleted</response>
        // [HttpDelete]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public async Task<IActionResult> Delete(string name)
        // {
        //     return await _mediator.Send(new DeletePayment(name));
        // }
    }
}