using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class RoleRepository : IRoleRepository
    {
        List<Role> roles = null;

        public RoleRepository() 
        {
            roles = this.GetRoles();
        }

        public Role Add(Role t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<Role>(t);

                if (ret != null)
                    roles.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Role already exists");
        }

        public void Delete(long id)
        {
            Role r = Get(id);
            DbStore.Delete<Role>(r);
            roles.Remove(r);
        }

        public Role Get(long id)
        {
            return roles.Find(d => d.Id == id);
        }

        public List<Role> Get()
        {
            return roles;
        }

        public Role Get(Expression<Func<Role, bool>> expression)
        {
            return roles.Find(expression.Compile().Invoke);
        }

        public List<Role> GetList(Expression<Func<Role, bool>> expression)
        {
            return roles.FindAll(expression.Compile().Invoke);
        }

        public List<Role> GetRoles()
        {
            if (roles == null)
            {
                var expression = PredicateBuilder.Create<Role>(d => d.DeletedDate == null);
                roles = DbStore.ExecuteeExpressionList<Role>(expression);
            }

            roles.Sort();
            return roles;
        }

        public Role Update(Role t)
        {
            if (!IsDuplicate(t))
            {

                if (t.Id <= 0)
                    throw new ArgumentException("Invalid Role Id");

                Role f = Get(t.Id);

                if (f == null)
                    throw new ArgumentNullException($"Role with id {t.Id} does not exist or its deleted");

                f.Name = t.Name;
                

                f = DbStore.Save<Role>(f);

                if (f != null)
                {
                    roles.Remove(t);
                    roles.Add(f);
                }

                return f;
            }
            else
                throw new DuplicateItemException("Role already exists");
        }

        bool IsDuplicate(Role r)
        {
            return roles.Exists(d => d.Name == r.Name);
        }
    }
}
