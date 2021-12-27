using MediatR;
using POC.API.Lib.Model;

namespace POC.API.Lib.Commands
{
    public class PostPayment : IRequest
    {
        public Payment Payment { get; set; }

        public PostPayment(Payment payment)
        {
            Payment = payment;
        }
    }
}