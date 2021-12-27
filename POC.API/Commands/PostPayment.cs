using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.API.Model;

namespace POC.API.Commands
{
    public class PostPayment : IRequest<IActionResult>
    {
        public Payment Payment { get; set; }

        public PostPayment(Payment payment)
        {
            Payment = payment;
        }
    }
}