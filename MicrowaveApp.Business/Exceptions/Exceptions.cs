using System;

namespace MicrowaveApp.Business.Exceptions
{
    public class InvalidPowerException : Exception
    {
        public InvalidPowerException(string message) : base(message) { }
    }

    public class InvalidTimeException : Exception
    {
        public InvalidTimeException(string message) : base(message) { }
    }
}
