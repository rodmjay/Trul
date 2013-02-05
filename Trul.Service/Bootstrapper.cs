using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Data.EntityFramework.Repositories;
using Trul.Domain.Repositories;
using Trul.Infrastructure.Crosscutting.IoC;

namespace Trul.Service
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Trul.Data.EntityFramework.Bootstrapper.Initialise();
            IoC.Container.Register(typeof(IMenuRepository), typeof(MenuRepository));
            IoC.Container.Register(typeof(ICountryRepository), typeof(CountryRepository));
            IoC.Container.Register(typeof(IPersonRepository), typeof(PersonRepository));
            IoC.Container.Register(typeof(IUserRepository), typeof(UserRepository));
            IoC.Container.Register(typeof(IRoleRepository), typeof(RoleRepository));
        }
    }
}
