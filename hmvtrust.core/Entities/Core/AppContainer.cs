using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hmvtrust.core.Application;
using Unity;
using Unity.Lifetime;

namespace hmvtrust.core.Entities
{
    public class AppContainer
    {
        static IUnityContainer container;

        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                    InitContainer();

                return container;
            }
        }

        private static void InitContainer()
        {
            container = new UnityContainer();

            container.RegisterType<IRoleRepository, RoleRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBranchRepository, BranchRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBookCategoryRepository, BookCategoryRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBookRepository, BookRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILibraryLogRepository, LibraryLogRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOfficeAuthenticationManager, OfficeAuthenticationManager>(new ContainerControlledLifetimeManager());
        }
    }
}
