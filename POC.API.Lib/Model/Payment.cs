using POC.Domain.Enums;
using DomainPayment = POC.Domain.PaymentAggregate.Payment;
using DomainDate = POC.Domain.PaymentAggregate.Date;
using DomainComment = POC.Domain.PaymentAggregate.Comment;

namespace POC.API.Lib.Model
{
    public class Payment
    {
        public string Name { get; set; }
        public Date Date { get; set; }
        public double Amount { get; set; }
        public ContactMethods ContactMethod { get; set; }
        public Comment Comment { get; set; }

        public Payment(string name, Date date, double amount, ContactMethods contactMethod, Comment comment)
        {
            Name = name;
            Date = date;
            Amount = amount;
            ContactMethod = contactMethod;
            Comment = comment;
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