using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Infrastructure.Crosscutting.IoC;

namespace Trul.Data.EntityFramework
{
    public class Bootstrapper
    {
        public static void Initialise() {
            IoC.Container.Register(typeof(UnitOfWork), typeof(UnitOfWork));
        }
    }
}
