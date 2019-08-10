using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class UserRepository : IUserRepository
    {

        public User Add(User t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                t.Password = string.IsNullOrEmpty(t.Password) ? Guid.NewGuid().ToString().Substring(0, 5) : t.Password;
                var ret = DbStore.Save<User>(t);

                return ret;
            }
            else
                throw new DuplicateItemException("Email already exists");
        }

        public void Delete(long id)
        {
            User r = Get(id);
            DbStore.Delete<User>(r);
        }

        public User Get(long id)
        {
            User user = null;
            if (user == null)
            {
                var expression = PredicateBuilder.Create<User>(d => d.Id == id && d.DeletedDate == null);
                user = DbStore.ExecuteExpressionFirst<User>(expression);
            }
            return user;
        }

        public List<User> Get()
        {
            List<User> userlist = null;
            var expression = PredicateBuilder.Create<User>(d => d.DeletedDate == null);
            userlist = DbStore.ExecuteeExpressionList<User>(expression);
            return userlist;
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            return DbStore.ExecuteExpressionFirst<User>(expression);
        }

        public List<User> GetList(Expression<Func<User, bool>> expression)
        {
            return DbStore.ExecuteeExpressionList<User>(expression);
        }

        public User Update(User t)
        {
            if (t.Id <= 0)
                throw new ArgumentException("Invalid User Id");

            User f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"User with id {t.Id} does not exist or its deleted");

            if(f.Email == t.Email)
            {
                f.FirstName = t.FirstName;
                f.MiddleName = t.MiddleName;
                f.LastName = t.LastName;
                f.Email = t.Email;
                f.UserName = t.UserName;
                f.Mobile = t.Mobile;
                f.AlternateContact = t.AlternateContact;
                f.Password = string.IsNullOrEmpty(t.Password) ? f.Password : t.Password;
                f.ProfilePhoto = string.IsNullOrEmpty(t.ProfilePhoto) ? f.ProfilePhoto : t.ProfilePhoto;
                f.Roles.Clear();

                f = DbStore.Save<User>(f);

                foreach (Role r in t.Roles)
                {
                    f.Roles.Add(r);
                }

                f = DbStore.Save<User>(f);
                return f;
            }
            else if (!IsDuplicate(t))
            {
                f.FirstName = t.FirstName;
                f.MiddleName = t.MiddleName;
                f.LastName = t.LastName;
                f.Email = t.Email;
                f.UserName = t.UserName;
                f.Mobile = t.Mobile;
                f.AlternateContact = t.AlternateContact;
                f.Password = string.IsNullOrEmpty(t.Password) ? f.Password : t.Password;
                f.ProfilePhoto = string.IsNullOrEmpty(t.ProfilePhoto) ? f.ProfilePhoto : t.ProfilePhoto;
                f.Roles.Clear();

                f = DbStore.Save<User>(f);

                foreach (Role r in t.Roles)
                {
                    f.Roles.Add(r);
                }

                f = DbStore.Save<User>(f);
                return f;
            }
            else
                throw new DuplicateItemException("Email already exists");
        }

        bool IsDuplicate(User r)
        {
            List<User> userslist = this.Get();
            return userslist.Exists(d => (d.UserName == r.UserName));
        }
    }
}
