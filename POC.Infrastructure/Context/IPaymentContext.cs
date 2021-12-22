using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Entity;

namespace POC.Infrastructure.Context
{
    public interface IPaymentContext
    {
        DbSet<Payment> Payments { get; set; }
        DbSet<Date> Dates { get; set; }
        DbSet<Comment> Comments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}