using System;

namespace POC.Domain.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException() : base(nameof(InfrastructureException))
        {

        }

        public InfrastructureException(string message) : base(message)
        {
            
        }
    }
}