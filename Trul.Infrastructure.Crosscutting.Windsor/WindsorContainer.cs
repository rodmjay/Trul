using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Trul.Infrastructure.Crosscutting.IoC;

namespace Trul.Infrastructure.Crosscutting.Windsor
{
    public class WindsorContainer : IContainer
    {
        public WindsorContainer() {
            container = new Castle.Windsor.WindsorContainer();
        }

        /// <summary>
        /// Resolve the target type with necessary dependencies.
        /// </summary>
        public object Resolve(Type targetType) {
            if(container.Kernel.HasComponent(targetType)) {
                return container.Resolve(targetType);
            }
            return null;
        }

        /// <summary>
        /// Resolves all registered instances for a specific service type.
        /// </summary>
        public IList<object> ResolveAll(Type serviceType) {
            if(container.Kernel.HasComponent(serviceType)) {
                return new List<object>((object[])container.ResolveAll(serviceType));
            }
            return null;
        }

        public readonly IWindsorContainer container;

        public T Resolve<T>() {
            return container.Resolve<T>();
        }

        public void Register(Type interfaceType, Type implementationType) {
            container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.PerWebRequest);
        }

        public void RegisterAllFromAssemblies(string a) {
            container.Register(AllTypes.FromAssemblyNamed(a).Pick().WithService.FirstInterface().LifestyleTransient());
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType) {
            container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Singleton);
        }


        public void Register(Type controllerType)
        {
            container.Register(Component.For(controllerType).LifeStyle.Transient);
        }
    }
}
