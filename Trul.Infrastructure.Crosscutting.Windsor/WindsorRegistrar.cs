using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Trul.Infrastructure.Crosscutting.IoC;

namespace Trul.Infrastructure.Crosscutting.Windsor
{
    public class WindsorRegistrar
    {
        public static void RegisterSingleton(Type interfaceType, Type implementationType) {
            Container().Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Singleton);
        }

        public static Castle.Windsor.IWindsorContainer Container() {
            WindsorContainer container = IoC.IoC.Container as WindsorContainer;
            return container.container;
        }
    }
}
