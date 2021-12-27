using System;
using POC.Domain.Enums;

namespace POC.API.Lib.Model
{
    public class Date
    {
        public DateTime NextPaymentDate { get; set; }
        public Frequency PaymentFrequency { get; set; }

        public Date(DateTime date, Frequency frequency)
        {
            NextPaymentDate = date;
            PaymentFrequency = frequency;
        }        
    }
}