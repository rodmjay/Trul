using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Core.Specification;
using Trul.Domain.Entities;

namespace Trul.Domain.Specifications
{
    public class MenuSpecifications
    {
        /// <summary>
        /// Specification for menu with application code like to <paramref name="text"/>
        /// </summary>
        /// <param name="text">The text to search</param>
        /// <returns>Associated specification for this criterion</returns>
        public static ISpecification<Menu> ApplicationCodeFullText(string text) {
            Specification<Menu> specification = new TrueSpecification<Menu>();

            if(!String.IsNullOrWhiteSpace(text)) {
                var nameSpecification = new DirectSpecification<Menu>(c => c.ApplicationCode.ToLower().Contains(text));

                specification &= nameSpecification;

            }

            return specification;
        }
    }
}
