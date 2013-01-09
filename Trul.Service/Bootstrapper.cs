using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Data.Repositories;
using Trul.Domain.Repositories;
using Trul.Infrastructure.Crosscutting.IoC;

namespace Trul.Service
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Trul.Data.Bootstrapper.Initialise();
            IoC.Container.Register(typeof(IMenuRepository), typeof(MenuRepository));
            IoC.Container.Register(typeof(ICountryRepository), typeof(CountryRepository));
            IoC.Container.Register(typeof(IPersonRepository), typeof(PersonRepository));
        }
    }
}
