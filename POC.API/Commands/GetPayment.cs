using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.API.Model;

namespace POC.API.Commands
{
    public class GetPayment : IRequest<IActionResult>
    {
        public string Name { get; set; }
        public GetPayment(string name)
        {
            Name = name;
        }
    }
}