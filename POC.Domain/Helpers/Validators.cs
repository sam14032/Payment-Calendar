using System;

namespace POC.Domain.Helpers
{
    public static class Validators
    {
        public static void Validate(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new Exception();
            }
        }

        public static void Validate(this double value)
        {
            if(value < 0)
            {
                throw new Exception();
            }
        }
    }
}