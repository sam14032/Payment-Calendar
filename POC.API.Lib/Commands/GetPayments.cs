using System.Collections.Generic;
using MediatR;
using POC.API.Lib.Model;

namespace POC.API.Lib.Commands
{
    public class GetPayments : IRequest<List<Payment>>
    {
        
    }
}