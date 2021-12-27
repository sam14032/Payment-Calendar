using System;

namespace POC.Domain.Exceptions
{
    public class LogicalException : Exception
    {
        public LogicalException() : base(nameof(LogicalException))
        {

        }

        public LogicalException(string message) : base(message)
        {
            
        }
    }
}