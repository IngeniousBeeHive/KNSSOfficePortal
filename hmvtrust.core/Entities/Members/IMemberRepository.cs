﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{    
    public interface IMemberRepository : IRepository<Member>
    {
        List<Member> GetMember();
    }
}
