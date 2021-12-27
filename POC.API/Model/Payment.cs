using POC.Domain.Enums;
using DomainPayment = POC.Domain.PaymentAggregate.Payment;
using DomainDate = POC.Domain.PaymentAggregate.Date;
using DomainComment = POC.Domain.PaymentAggregate.Comment;

namespace POC.API.Model
{
    public class Payment
    {
        public string Name { get; set; }
        public Date Date { get; set; }
        public double Amount { get; set; }
        public ContactMethods ContactMethod { get; set; }
        public Comment Comment { get; set; }

        public Payment()
        {

        }

        public DomainPayment ToDomain()
        {
            return new DomainPayment(
                Name,
                new DomainDate(
                    Date.NextPaymentDate,
                    Date.PaymentFrequency
                ),
                Amount,
                ContactMethod,
                new DomainComment(Comment.Text)
            );
        }
    }
}