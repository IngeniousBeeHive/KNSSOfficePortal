using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public interface IRoleRepository : IRepository<Role>
    {
        List<Role> GetRoles();
    }
}
