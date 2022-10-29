using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using webshop.Util;

namespace webshop
{
    public class DependencyContainer
    {
        private static DependencyContainer @default;

        public static DependencyContainer Default
        {
            get
            {
                if (@default == null)
                {
                    @default = new DependencyContainer();
                }

                return @default;
            }
        }

        private Container container;

        public Container Container
        {
            get
            {
                if (container == null)
                {
                    container = CreateContainer();
                }

                return container;
            }
        }

        private Container CreateContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(
                defaultLifestyle: new WebRequestLifestyle(),
                fallbackLifestyle: new ThreadScopedLifestyle()
            );

            //Register dependencies
            container.Register(() => new ApplicationDbContext(), Lifestyle.Scoped);

            container.Register<IConfig, Config>(Lifestyle.Singleton);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }
    }
}