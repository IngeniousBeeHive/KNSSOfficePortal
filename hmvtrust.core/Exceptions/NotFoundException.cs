using System;
using System.Collections.Generic;
using System.Text;

namespace hmvtrust.core
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception excption) : base(message, excption) { }
        public NotFoundException(long id, string message, Exception excption) : base(message, excption) { }
    }
}
