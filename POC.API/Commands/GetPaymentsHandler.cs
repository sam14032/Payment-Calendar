using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.Domain.Repository;

namespace POC.API.Commands
{
    public class GetPaymentsHandler : IRequestHandler<GetPayments, IActionResult>
    {
        public IPaymentRepository _repository;

        public GetPaymentsHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetPayments request, CancellationToken cancellationToken)
        {
            var payments = await _repository.GetPayments();

            return new OkObjectResult(payments);
        }
        
    }
}