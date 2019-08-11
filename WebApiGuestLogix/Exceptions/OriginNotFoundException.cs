using System;
using System.Runtime.Serialization;

namespace WebApiGuestLogix.Exceptions
{
    [Serializable]
    public class OriginNotFoundException : Exception
    {
        public OriginNotFoundException()
        {
        }

        public OriginNotFoundException(string message) : base(message)
        {
        }

        public OriginNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OriginNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}