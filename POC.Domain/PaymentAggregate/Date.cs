using System;
using POC.Domain.Enums;

namespace POC.Domain.PaymentAggregate
{
    public class Date
    {
        public DateTime NextPaymentDate { get; set; }
        public Frequency PaymentFrequency { get; set; }

        public Date(DateTime nextPaymentDate, Frequency paymentFrequency)
        {
            NextPaymentDate = nextPaymentDate;
            PaymentFrequency = paymentFrequency;
        }
    }
}