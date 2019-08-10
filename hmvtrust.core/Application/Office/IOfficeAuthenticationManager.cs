using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Application
{
    public interface IOfficeAuthenticationManager 
    {
        User Authenticate(string username, string password);
    }
}
