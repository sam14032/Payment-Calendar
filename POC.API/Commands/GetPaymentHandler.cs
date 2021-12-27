using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.Domain.Repository;

namespace POC.API.Commands
{
    public class GetPaymentHandler : IRequestHandler<GetPayment, IActionResult>
    {
        public IPaymentRepository _repository;

        public GetPaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetPayment request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetPayment(request.Name);

            return new OkObjectResult(payment);
        }
        
    }
}