using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using Trul.Infrastructure.Crosscutting.Windsor;
using Trul.Application.UI.Core.Tasks;
using Trul.Application.UI.Tasks;

namespace Trul.Mobile.IoC
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Trul.Infrastructure.Crosscutting.IoC.IoC.SetContainer(new Trul.Infrastructure.Crosscutting.Windsor.WindsorContainer());
            var container = Trul.Infrastructure.Crosscutting.IoC.IoC.Container as Trul.Infrastructure.Crosscutting.Windsor.WindsorContainer;
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.container));
            Trul.Application.UI.Bootstrapper.Initialise();
            Configure();

            WindsorServiceLocator locator = new WindsorServiceLocator(container.container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private static void Configure() {
            Trul.Infrastructure.Crosscutting.IoC.IoC.Container.Register(typeof(IHomeTask), typeof(HomeTask));
        }
    }
}