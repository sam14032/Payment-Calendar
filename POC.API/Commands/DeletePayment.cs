using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.API.Model;

namespace POC.API.Commands
{
    public class DeletePayment : IRequest<IActionResult>
    {
        public string Name { get; set; }
        public DeletePayment(string name)
        {
            Name = name;
        }
    }
}