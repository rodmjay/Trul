using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Infrastructure.Crosscutting.IoC;
using Trul.Service;
using Trul.Service.Core;

namespace Trul.Application.UI
{
    public class Bootstrapper
    {
        public static void Initialise()
        {
            InitialiseIoC();
            Trul.Service.Bootstrapper.Initialise();
            IoC.Container.Register(typeof(ICountryService), typeof(CountryService));
            IoC.Container.Register(typeof(IPersonService), typeof(PersonService));
            IoC.Container.Register(typeof(IMenuService), typeof(MenuService));
            IoC.Container.Register(typeof(IUserService), typeof(UserService));
            IoC.Container.Register(typeof(IRoleService), typeof(RoleService));
        }

        private static void InitialiseIoC()
        {
            Trul.Infrastructure.Crosscutting.IoC.IoC.SetContainer(new Trul.Infrastructure.Crosscutting.Windsor.WindsorContainer());
            var container = Trul.Infrastructure.Crosscutting.IoC.IoC.Container as Trul.Infrastructure.Crosscutting.Windsor.WindsorContainer;

            WindsorServiceLocator locator = new WindsorServiceLocator(container.container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
