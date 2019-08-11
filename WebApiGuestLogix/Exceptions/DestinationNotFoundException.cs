using System;
using System.Runtime.Serialization;

namespace WebApiGuestLogix.Exceptions
{
    [Serializable]
    public class DestinationNotFoundException : Exception
    {
        public DestinationNotFoundException()
        {
        }

        public DestinationNotFoundException(string message) : base(message)
        {
        }

        public DestinationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DestinationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}