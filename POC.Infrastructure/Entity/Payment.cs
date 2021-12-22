using System.ComponentModel.DataAnnotations.Schema;
using POC.Domain.Enums;

namespace POC.Infrastructure.Entity
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public ContactMethods ContactMethod { get; set; }
        public Date Date { get; set; }
        public Comment Comment { get; set; }
    }
}