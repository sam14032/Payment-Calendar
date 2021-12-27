using MediatR;
using POC.API.Lib.Model;

namespace POC.API.Lib.Commands
{
    public class GetPayment : IRequest<Payment>
    {
        public string Name { get; set; }
        
        public GetPayment(string name)
        {
            Name = name;
        }
    }
}