using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.UI.Task;
using Trul.Infrastructure.Crosscutting.IoC;
using Trul.Service;
using Trul.Service.Core;

namespace Trul.Application.UI
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Trul.Service.Bootstrapper.Initialise();
            IoC.Container.Register(typeof(ICountryService), typeof(CountryService));
            IoC.Container.Register(typeof(IPersonService), typeof(PersonService));
            IoC.Container.Register(typeof(IMenuService), typeof(MenuService));
        }
    }
}
