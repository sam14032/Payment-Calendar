using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.Domain.Repository;

namespace POC.API.Commands
{
    public class PostPaymentHandler : IRequestHandler<PostPayment, IActionResult>
    {
        public IPaymentRepository _repository;

        public PostPaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(PostPayment request, CancellationToken cancellationToken)
        {
            await _repository.AddPayment(request.Payment.ToDomain());

            return new OkResult();
        }
    }
}