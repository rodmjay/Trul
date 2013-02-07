using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application;
using Trul.Application.DTO;
using Trul.Domain.Core;
using Trul.Domain.Entities;
using Trul.Domain.Specifications;
using Trul.Framework.Security;

namespace Trul.Service
{
    public partial class UserService
    {
        public string GetPasswordByUserName(string userName)
        {
            return Repository.AllMatching(UserSpecifications.UserName(userName)).Select(m => m.Password).FirstOrDefault();
        }

        public void ChangePassword(UserDTO user, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("password cannot bu null");

            var dbUser = GetUserByUserName(user.UserName);
            dbUser.Password = password;
            Repository.Modify(dbUser);
        }

        private User GetUserByUserName(string userName, bool isThowException = true)
        {
            var user = this.Repository.AllMatching(UserSpecifications.UserName(userName), m => m.Roles).FirstOrDefault();
            if (user == null && isThowException) throw new Exception("User Not Found");

            return user;
        }

        public UserDTO GetByUserName(string userName)
        {
            return GetUserByUserName(userName).ProjectedAs<UserDTO, int>();
        }

        public UserDTO Register(UserDTO user, string password)
        {
            var entity = GetUserByUserName(user.UserName, false);
            if (entity != null) throw new Exception("User is already registered");

            var dbUser = user.ProjectedAs<User, int>();

            dbUser.Password = PasswordHash.CreateHash(password);
            Repository.Add(dbUser);

            Repository.UnitOfWork.Commit();

            return dbUser.ProjectedAs<UserDTO, int>();
        }
    }
}
