using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Core.Specification;
using Trul.Domain.Entities;

namespace Trul.Domain.Specifications
{
    public class PersonSpecifications
    {
        public static ISpecification<Person> NameFullText(string text)
        {
            Specification<Person> specification = new TrueSpecification<Person>();

            if (!string.IsNullOrWhiteSpace(text))
            {
                var firstNameSpecification = new DirectSpecification<Person>(c => c.FirstName.ToLower().Contains(text.ToLower()));
                var lastNameSpecification = new DirectSpecification<Person>(c => c.LastName.ToLower().Contains(text.ToLower()));

                specification &= (firstNameSpecification || lastNameSpecification);
            }

            return specification;
        }
    }
}
