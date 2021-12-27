using System.Threading;
using System.Threading.Tasks;
using MediatR;
using POC.Domain.Exceptions;
using POC.Domain.Repository;

namespace POC.API.Lib.Commands
{
    public class DeletePaymentHandler : IRequestHandler<DeletePayment>
    {
        private IPaymentRepository _repository;

        public DeletePaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeletePayment request, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrEmpty(request.Name))
            {
                throw new LogicalException("Payment name is missing");
            }
            
            await _repository.DeletePayment(request.Name);

            return Unit.Value;
        }
        
    }
}