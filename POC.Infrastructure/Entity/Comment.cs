using System.ComponentModel.DataAnnotations.Schema;

namespace POC.Infrastructure.Entity
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Text { get; set; }
        
        public Payment Payment { get; set; }
    }
}