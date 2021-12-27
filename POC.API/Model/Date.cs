using System;
using POC.Domain.Enums;

namespace POC.API.Model
{
    public class Date
    {
        public DateTime NextPaymentDate { get; set; }
        public Frequency PaymentFrequency { get; set; }
    }
}