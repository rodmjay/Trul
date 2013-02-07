using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.DTO;

namespace Trul.Service.Core
{
    public partial interface IUserService
    {
        UserDTO GetByUserName(string userName);

        string GetPasswordByUserName(string userName);

        void ChangePassword(UserDTO user, string password);

        UserDTO Register(UserDTO user, string password);
    }
}
