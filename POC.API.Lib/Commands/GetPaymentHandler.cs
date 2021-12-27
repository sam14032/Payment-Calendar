using System.Threading;
using System.Threading.Tasks;
using MediatR;
using POC.API.Lib.Model;
using POC.Domain.Exceptions;
using POC.Domain.Repository;

namespace POC.API.Lib.Commands
{
    public class GetPaymentHandler : IRequestHandler<GetPayment, Payment>
    {
        private IPaymentRepository _repository;

        public GetPaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Payment> Handle(GetPayment request, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrEmpty(request.Name))
            {
                throw new LogicalException("Payment name is missing");
            }
            
            var payment = await _repository.GetPayment(request.Name);

            return 
                new Payment (
                        payment.Name,
                        new Date(payment.Date.NextPaymentDate, payment.Date.PaymentFrequency),
                        payment.Amount,
                        payment.ContactMethod,
                        new Comment(payment.Comment.Text)
                    );
        }
    }
}