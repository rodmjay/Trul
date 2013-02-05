using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Core.Specification;
using Trul.Domain.Entities;

namespace Trul.Domain.Specifications
{
    public class UserSpecifications
    {
        /// <summary>
        /// Specification for menu with application code like to <paramref name="text"/>
        /// </summary>
        /// <param name="text">The text to search</param>
        /// <returns>Associated specification for this criterion</returns>
        public static ISpecification<User> UserName(string userName) {
            Specification<User> specification = new TrueSpecification<User>();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                var nameSpecification = new DirectSpecification<User>(c => c.UserName.ToLower() == userName.ToLower());

                specification &= nameSpecification;
            }

            return specification;
        }
    }
}
