using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application;
using Trul.Application.DTO;
using Trul.Domain.Core;
using Trul.Domain.Specifications;

namespace Trul.Service
{
    public partial class UserService
    {
        public UserDTO GetByUserName(string userName)
        {
            UserDTO result = null;
            var user = this.Repository.AllMatching(UserSpecifications.UserName(userName), m => m.Roles).FirstOrDefault();
            if (user != null)
            {
                result = ((IEntityWithTypedId<int>)user).ProjectedAs<UserDTO, int>();
            }
            return result;
        }
    }
}
