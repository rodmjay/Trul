using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Trul.Mobile.IoC
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container) {
            this.container = container;
            var controllerTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where typeof(IController).IsAssignableFrom(t)
                select t;
            foreach(var t in controllerTypes)
                container.Register(Component.For(t).LifeStyle.Transient);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            if(controllerType == null) return null;
            return (IController)container.Resolve(controllerType);
        }
    }
}