using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hmvtrust.core
{
    public class InvalidDataException : ApplicationException
    {
        public InvalidDataException()
        {
        }

        public InvalidDataException(string message) : base(message)
        {
        }

        public InvalidDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
