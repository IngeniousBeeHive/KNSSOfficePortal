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
    public class RoleRepositoryTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Role role = new Role();
            role.Name = "SysAdmin";


            var roleRepository = new RoleRepository();

            role = roleRepository.Add(role);
        }

        [TestMethod]
        public void GetTest()
        {
            long i = 2;
            Role role = new Role();

            var roleRepository = new RoleRepository();

            role = roleRepository.Get(i);

            Assert.IsNotNull(role);
        }

        [TestMethod]
        public void GetTestList()
        {
            List<Role> role = new List<Role>();

            var roleRepository = new RoleRepository();

            role = roleRepository.Get();

            Assert.IsNotNull(role);
        }

        [TestMethod]
        public void GetExpressionTest()
        {
            long i = 2;
            Role role = new Role();
            var exp = PredicateBuilder.Create<Role>(d => d.Id == i && d.DeletedDate == null);
            var roleRepository = new RoleRepository();

            role = roleRepository.Get(exp);

            Assert.IsNotNull(role);
        }

        [TestMethod]
        public void GetExpressionTestList()
        {
            List<Role> role = new List<Role>();
            var exp = PredicateBuilder.Create<Role>(d => d.Name == "SysAdmin" && d.DeletedDate == null);
            var roleRepository = new RoleRepository();

            role = roleRepository.GetList(exp);
        }

        [TestMethod]
        public void UpdateTest()
        {
            long i = 2;

            Role role = new Role();

            var roleRepository = new RoleRepository();

            role = roleRepository.Get(i);

            Assert.IsNotNull(role);

            role.Name = "Administrator";

            role = roleRepository.Update(role);
        }

        [TestMethod]
        public void DeleteTest()
        {
            long i = 1;

            Role role = new Role();

            var roleRepository = new RoleRepository();

            roleRepository.Delete(i);
        }
    }
}