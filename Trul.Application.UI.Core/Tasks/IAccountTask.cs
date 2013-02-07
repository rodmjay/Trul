using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.UI.Core.Models;

namespace Trul.Application.UI.Core.Tasks
{
    public interface IAccountTask
    {
        void Login(AccountViewModel model);

        void Register(AccountViewModel model);

        void LogOut();
    }
}
