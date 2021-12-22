using System;
using System.ComponentModel.DataAnnotations.Schema;
using POC.Domain.Enums;

namespace POC.Infrastructure.Entity
{
    public class Date
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public Frequency PaymentFrequency { get; set; }

        public Payment Payment { get; set; }
    }
}