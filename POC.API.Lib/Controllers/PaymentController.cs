using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using POC.API.Lib.Commands;
using POC.API.Lib.Model;

namespace POC.API.Lib.Controllers
{
    public class PaymentController
    {
        private IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Post(Payment payment)
        {
            await _mediator.Send(new PostPayment(payment));
        }

        public async Task<List<Payment>> Get()
        {
            return await _mediator.Send(new GetPayments());
        }
        
        public async Task<Payment> Get(string name)
        {
            return await _mediator.Send(new GetPayment(name));
        }
        
        public async Task Delete(string name)
        {
            await _mediator.Send(new DeletePayment(name));
        }
    }
}