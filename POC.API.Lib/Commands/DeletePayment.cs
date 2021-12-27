using MediatR;

namespace POC.API.Lib.Commands
{
    public class DeletePayment : IRequest
    {
        public string Name { get; set; }
        
        public DeletePayment(string name)
        {
            Name = name;
        }
    }
}