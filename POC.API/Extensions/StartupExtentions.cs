using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POC.Infrastructure.Context;

namespace POC.API.Extensions
{
    public static class StartupExtentions
    {
        public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            services.AddDbContext<IPaymentContext ,PaymentContext>(option => option.UseSqlite(connection));
        }

        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PaymentContext>();
            context.Database.Migrate();
        }
    }
}