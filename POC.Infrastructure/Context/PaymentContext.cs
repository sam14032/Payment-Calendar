using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Entity;

namespace POC.Infrastructure.Context
{
    public class PaymentContext : DbContext, IPaymentContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<Payment>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Date)
                .WithOne(x => x.Payment)
                .HasForeignKey<Date>(x => x.Id);

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Comment)
                .WithOne(x => x.Payment)
                .HasForeignKey<Comment>(x => x.Id);

            modelBuilder.Entity<Date>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id);
        }
    }
}