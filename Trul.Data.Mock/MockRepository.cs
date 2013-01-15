using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;

namespace Trul.Data.Mock
{
    public class MockRepository
    {
        public IEnumerable<Country> GetAll()
        {
            var countries = new List<Country>();
            countries.Add(new Country { ID = 1, Name = "Country 1" });
            countries.Add(new Country { ID = 2, Name = "Country 2" });
            countries.Add(new Country { ID = 3, Name = "Country 3" });
            countries.Add(new Country { ID = 4, Name = "Country 4" });
            return countries;
        }
    }
}
