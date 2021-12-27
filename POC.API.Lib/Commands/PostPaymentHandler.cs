using System.Threading;
using System.Threading.Tasks;
using MediatR;
using POC.Domain.Exceptions;
using POC.Domain.Repository;

namespace POC.API.Lib.Commands
{
    public class PostPaymentHandler : IRequestHandler<PostPayment>
    {
        private IPaymentRepository _repository;

        public PostPaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(PostPayment request, CancellationToken cancellationToken = default)
        {
            if(request.Payment == null)
            {
                throw new LogicalException("There is no payment to process");
            }
            
            await _repository.AddPayment(request.Payment.ToDomain());

            return Unit.Value;
        }
    }
}