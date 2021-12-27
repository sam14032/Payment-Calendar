using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.Domain.Repository;

namespace POC.API.Commands
{
    public class DeletePaymentHandler : IRequestHandler<DeletePayment, IActionResult>
    {
        public IPaymentRepository _repository;

        public DeletePaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(DeletePayment request, CancellationToken cancellationToken)
        {
            await _repository.DeletePayment(request.Name);

            return new OkResult();
        }
        
    }
}