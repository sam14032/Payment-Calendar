using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using POC.API.Lib.Model;
using POC.Domain.Repository;

namespace POC.API.Lib.Commands
{
    public class GetPaymentsHandler : IRequestHandler<GetPayments, List<Payment>>
    {
        private IPaymentRepository _repository;

        public GetPaymentsHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Payment>> Handle(GetPayments request, CancellationToken cancellationToken = default)
        {
            var payments = await _repository.GetPayments();

            return 
                payments.Select(
                    x => new Payment (
                        x.Name,
                        new Date(x.Date.NextPaymentDate, x.Date.PaymentFrequency),
                        x.Amount,
                        x.ContactMethod,
                        new Comment(x.Comment.Text)
                    )
                ).ToList();
        }        
    }
}