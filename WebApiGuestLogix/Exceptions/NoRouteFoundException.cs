using System;
using System.Runtime.Serialization;

namespace WebApiGuestLogix.Exceptions
{
    [Serializable]
    public class NoRouteFoundException : Exception
    {
        public NoRouteFoundException()
        {
        }

        public NoRouteFoundException(string message) : base(message)
        {
        }

        public NoRouteFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRouteFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}