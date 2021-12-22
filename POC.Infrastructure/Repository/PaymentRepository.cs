using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainPayment = POC.Domain.PaymentAggregate.Payment;
using DomainDate = POC.Domain.PaymentAggregate.Date;
using DomainComment = POC.Domain.PaymentAggregate.Comment;
using POC.Domain.Repository;
using POC.Infrastructure.Context;
using POC.Infrastructure.Entity;
using System.Linq;

namespace POC.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private IPaymentContext _db;

        public PaymentRepository(IPaymentContext context)
        {
            _db = context;
        }

        public async Task AddPayment(DomainPayment payment)
        {
            var existing = await FirstOrDefault(payment.Name);

            if (existing != null)
            {
                existing.Name = payment.Name;
                existing.Amount = payment.Amount;
                existing.ContactMethod = payment.ContactMethod;
                existing.Date =
                    new Date
                    {
                        Id = existing.Date.Id,
                        NextPaymentDate = payment.Date.NextPaymentDate,
                        PaymentFrequency = payment.Date.PaymentFrequency
                    };
                existing.Comment = new Comment { Id = existing.Comment.Id, Text = payment.Comment.Text };
            }
            else
            {
                existing =
                    new Payment
                    {
                        Name = payment.Name,
                        Amount = payment.Amount,
                        ContactMethod = payment.ContactMethod,
                        Date =
                        new Date
                        {
                            NextPaymentDate = payment.Date.NextPaymentDate,
                            PaymentFrequency = payment.Date.PaymentFrequency
                        },
                        Comment = new Comment { Text = payment.Comment.Text }
                    };
            }
            _db.Payments.Update(existing);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePayment(string name)
        {
            var payment = await FirstOrDefault(name);
            if(payment != null)
            {
                _db.Payments.Remove(payment);
                _db.Dates.Remove(payment.Date);
                _db.Comments.Remove(payment.Comment);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<DomainPayment> GetPayment(string name)
        {
            DomainPayment domainPayment = null;
            var payment = await FirstOrDefault(name);

            if(payment != null)
            {
                domainPayment = 
                    new DomainPayment(
                        payment.Name,
                        new DomainDate(
                            payment.Date.NextPaymentDate,
                            payment.Date.PaymentFrequency
                        ),
                        payment.Amount,
                        payment.ContactMethod,
                        new DomainComment(payment.Comment.Text)
                    );
            }

            return domainPayment;
        }

        public async Task<List<DomainPayment>> GetPayments()
        {
            var payments = await _db.Payments
                .Include(x => x.Date)
                .Include(x => x.Comment).ToListAsync<Payment>();

            return payments.Select(x =>
                    new DomainPayment(
                        x.Name,
                        new DomainDate(
                            x.Date.NextPaymentDate,
                            x.Date.PaymentFrequency
                        ),
                        x.Amount,
                        x.ContactMethod,
                        new DomainComment(x.Comment.Text)
                    )
                ).ToList<DomainPayment>();
        }

        private async Task<Payment> FirstOrDefault(string name)
        {
            return await _db.Payments.Where(x => x.Name == name).Include(x => x.Date).Include(x => x.Comment).FirstOrDefaultAsync();
        }

        private async Task<Date> FirstOrDefaultDate(long id)
        {
            return await _db.Dates.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        private async Task<Comment> FirstOrDefaultComment(long id)
        {
            return await _db.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}