using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hmvtrust.core.Exceptions
{
    public class MissingDataException : ApplicationException
    {
        public MissingDataException()
        {
        }

        public MissingDataException(string message) : base(message)
        {
        }

        public MissingDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
