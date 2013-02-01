using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Infrastructure.Crosscutting.IoC
{
    public interface IContainer
    {
        T Resolve<T>();
        object Resolve(Type type);
        void Register(Type controllerType);
        void Register(Type interfaceType, Type implementationType);
        void RegisterAllFromAssemblies(string a);
        void RegisterSingleton(Type interfaceType, Type implementationType);
    }
}
