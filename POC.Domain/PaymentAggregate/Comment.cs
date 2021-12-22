using POC.Domain.Helpers;

namespace POC.Domain.PaymentAggregate
{
    public class Comment
    {
        public string Text { get; set; }

        public Comment(string text)
        {
            text.Validate();
            Text = text;
        }        
    }
}