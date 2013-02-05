using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Trul.Application.UI.Core.Models;
using Trul.Application.UI.Core.Tasks;
using Trul.Framework.Security;
using Trul.Infrastructure.Crosscutting.Security;
using Trul.Service.Core;

namespace Trul.Application.UI.Tasks
{
    public class AccountTask : TaskBase, IAccountTask
    {
        private IUserService userService;

        public AccountTask(IUserService userService)
        {
            this.userService = userService;
        }

        public void Login(AccountViewModel model)
        {
            var user = userService.GetByUserName(model.UserName);
            if (user != null && user.Password == model.Password)
            {
                var authenticationService = AuthenticationFactory.CreateAuthentication();

                var serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserID = user.ID;
                //serializeModel.FirstName = user.FirstName;
                //serializeModel.LastName = user.LastName;
                serializeModel.UserName = user.UserName;
                serializeModel.Roles = user.Roles.Select(r => r.ID.ToString()).ToArray();

                var serializer = new JavaScriptSerializer();
                var userData = serializer.Serialize(serializeModel);

                authenticationService.Login(model.UserName, model.Password, model.IsRememberMe, userData);
            }
        }

        public void LogOut()
        {
            AuthenticationFactory.CreateAuthentication().Logout();
        }
    }
}
