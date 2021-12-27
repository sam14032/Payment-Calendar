using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.API.Model;

namespace POC.API.Commands
{
    public class GetPayments : IRequest<IActionResult>
    {
        public GetPayments()
        {

        }
    }
}