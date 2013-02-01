using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trul.WebUI.IoC
{
    public class ControllerFactory : DefaultControllerFactory
    {
        public ControllerFactory() {
            var controllerTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where typeof(IController).IsAssignableFrom(t)
                select t;
            foreach (var t in controllerTypes)
            {
                Trul.Infrastructure.Crosscutting.IoC.IoC.Container.Register(t);
            }
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            if(controllerType == null) return null;
            return (IController)Trul.Infrastructure.Crosscutting.IoC.IoC.Resolve(controllerType);
        }
    }
}