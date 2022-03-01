using System;
using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POC.API.Lib.Commands;
using POC.Domain.Repository;
using POC.Infrastructure.Context;
using POC.Infrastructure.Repository;

namespace POC.API.Lib
{
    public class Startup
    {
        private readonly string _connectionString;
        private static IServiceCollection _services;
        private static IServiceProvider _serviceProvider;

        public static IMediator Mediator { get; set; }

        public Startup(IServiceCollection services, string connectionString)
        {
            _services = services;
            _connectionString = connectionString;
        }

        public void ConfigureServices()
        {
            RegisterServices();
            ConfigureDatabase(_connectionString);
            Mediator = _serviceProvider.GetRequiredService<IMediator>();
        }

        private void RegisterServices()
        {
            _services.AddMediatR(Assembly.GetExecutingAssembly());

            _services.AddTransient<PaymentContext>();
            _services.AddTransient<IPaymentContext, PaymentContext>();
            _services.AddTransient<IPaymentRepository, PaymentRepository>();
        }

        private void ConfigureDatabase(string connectionString)
        {
            var connection = new SqliteConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                connection.Close();
            }
            
            var services = _services.AddDbContext<IPaymentContext ,PaymentContext>(option => option.UseSqlite(connection));
            _serviceProvider = services.BuildServiceProvider();

            var context = _serviceProvider.GetRequiredService<PaymentContext>();

            context.Database.Migrate();
        }
    }
}
