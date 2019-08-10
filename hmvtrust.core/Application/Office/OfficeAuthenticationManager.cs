using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using hmvtrust.core.Entities;

namespace hmvtrust.core.Application
{
    public class OfficeAuthenticationManager : IOfficeAuthenticationManager
    {
        IUserRepository userRepository;

        public OfficeAuthenticationManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            User user = null;

            if (!(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
            {
                var expression = PredicateBuilder.Create<User>(u => u.UserName == username
                                                                 && u.Password == password
                                                                 && u.IsLocked == false
                                                                 && u.DeletedDate == null);

                user = userRepository.Get(expression);

                if (user != null && (!user.Password.Equals(password, StringComparison.Ordinal)))
                    user = null; 

                if (user != null)
                {
                    GenericIdentity identity = new GenericIdentity(user.Id.ToString());
                    GenericPrincipal principal = new GenericPrincipal(identity, null);
                    Thread.CurrentPrincipal = principal;

                    user.LastLoginTime = DateTime.Now;
                    DbStore.Save<User>(user);
                }
            }
            else
            {
                Exception excp = new Exception("Invalid user name / password");
                throw excp;
            }

            return user;
        }
    }
}
