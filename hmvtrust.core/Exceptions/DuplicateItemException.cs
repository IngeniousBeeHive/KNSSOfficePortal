using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hmvtrust.core.Exceptions
{
    public class DuplicateItemException : ApplicationException
    {
        string itemType = string.Empty;

        public DuplicateItemException()
        {
        }

        public DuplicateItemException(string message) : base(message)
        {
        }

        public DuplicateItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateItemException(string itemType,string message, Exception innerException) : base(message, innerException)
        {
            this.itemType = itemType;
        }

        protected DuplicateItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
