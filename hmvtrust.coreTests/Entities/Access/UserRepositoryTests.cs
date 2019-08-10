using Microsoft.VisualStudio.TestTools.UnitTesting;
using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Role role = new Role();
            var roleRepository = new RoleRepository();
            var userRepository = new UserRepository();

            long i = 2;
            role = roleRepository.Get(i);

            User user = new User();
            user.FirstName = "Sen";
            user.MiddleName = null;
            user.LastName = "Vel";
            user.Email = "Sen@attristech.com";
            user.UserName = "Sen@attristech.com";
            user.Mobile = "9874563210";
            user.AlternateContact = null;
            user.Password = "2";
            user.ProfilePhoto = null;
            user.LastLoginTime = null;
            user.IsLocked = false;
            user.IsInternal = false;

            user.Roles.Add(role);

            userRepository.Add(user);
        }

        [TestMethod]
        public void GetTest()
        {
            long i = 1;

            User user = new User();

            var userRepository = new UserRepository();

            user = userRepository.Get(i);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetTestList()
        {
            List<User> users = new List<User>();

            var userRepository = new UserRepository();

            users = userRepository.Get();

            Assert.IsNotNull(users);
        }

        [TestMethod]
        public void GetExpressionTest()
        {
            long i = 1;
            User user = new User();
            var exp = PredicateBuilder.Create<User>(d => d.Id == i && d.DeletedDate == null);
            var userRepository = new UserRepository();

            user = userRepository.Get(exp);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetExpressionTestList()
        {
            List<User> users = null;
            var exp = PredicateBuilder.Create<User>(d => d.UserName == "Senthil@attristech.com" && d.DeletedDate == null);
            var userRepository = new UserRepository();

            users = userRepository.GetList(exp);
        }

        [TestMethod]
        public void UpdateTest()
        {
            long i = 1;

            User user = new User();

            var userRepository = new UserRepository();

            user = userRepository.Get(i);

            Assert.IsNotNull(user);

            user.UserName = "Senthil@attristech.com";

            user = userRepository.Update(user);
        }

        [TestMethod]
        public void DeleteTest()
        {
            long i = 2;

            User user = new User();

            var userRepository = new UserRepository();

            userRepository.Delete(i);
        }
    }
}