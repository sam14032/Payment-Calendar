using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using POC.API.Lib;
using POC.API.Lib.Controllers;
using POC.API.Lib.Model;
using POC.Domain.Enums;

namespace HelloWorld
{
    class Program
    {
        static ServiceCollection services;
        static IMediator mediator;
        static PaymentController paymentController;
        static async Task Main(string[] args)
        {
            services = new ServiceCollection();
            Startup startup = new Startup(services, "DataSource=C:/Dev/fall2021/POC/POC.Infrastructure/DB/migration.db;mode=ReadWriteCreate;cache=shared");
            startup.ConfigureServices();
            paymentController = new PaymentController();
            await paymentController.Post(new Payment("name", null, 12, ContactMethods.Online, null));
        }
    }
}