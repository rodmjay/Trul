using System.Web.Mvc;
using Trul.Application.UI.Core.Tasks;
using Trul.Application.UI.Tasks;

namespace Trul.WebUI.IoC
{
    public class Bootstrapper
    {
        public static void Initialise()
        {
            Trul.Application.UI.Bootstrapper.Initialise();
            Configure();

            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
        }

        private static void Configure() {
            Trul.Infrastructure.Crosscutting.IoC.IoC.Container.Register(typeof(IHomeTask), typeof(HomeTask));
            Trul.Infrastructure.Crosscutting.IoC.IoC.Container.Register(typeof(ICountryTask), typeof(CountryTask));
            Trul.Infrastructure.Crosscutting.IoC.IoC.Container.Register(typeof(IAccountTask), typeof(AccountTask));
        }
    }
}