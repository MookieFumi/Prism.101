using System;
using System.Runtime.Serialization;

namespace NBAStats.Core.Services.Exceptions
{
    [Serializable]
    public class NotConnectivityException : Exception
    {
        public NotConnectivityException()
        {
        }

        public NotConnectivityException(string message) : base(message)
        {
        }

        public NotConnectivityException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotConnectivityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
