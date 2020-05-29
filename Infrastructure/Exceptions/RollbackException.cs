using System;
using System.Runtime.Serialization;

namespace Framework.Infrastructure.Exceptions
{
    [Serializable]
    public class RollbackException : Exception
    {
        public RollbackException(string message)
            : base(message)
        {
        }

        public RollbackException()
            : base()
        {
        }

        public RollbackException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RollbackException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
