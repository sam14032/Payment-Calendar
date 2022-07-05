using System.Collections.Generic;
using System.Threading.Tasks;
using POC.Domain.PaymentAggregate;

namespace POC.Domain.Repository
{
    public interface IPaymentRepository
    {
        Task AddPayment(Payment payment);
        Task<Payment> GetPayment(string name);
        Task<List<Payment>> GetPayments();
        Task<int> DeletePayment(string name);
    }
}