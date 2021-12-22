using POC.Domain.Enums;
using POC.Domain.Helpers;

namespace POC.Domain.PaymentAggregate
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
            name.Validate();
            amount.Validate();

            Name = name;
            Date = date;
            ContactMethod = contactMethod;
            Comment = comment;
            Amount = amount;
        }
    }
}
