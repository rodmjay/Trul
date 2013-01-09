using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trul.Service.Core;
using System.Linq;
using Trul.Data.Repositories;

namespace Trul.Service.Test
{
    [TestClass]
    public class RepositoryServiceTest
    {
        private ICountryService countryService;
        private IMenuService menuService;
        private IPersonService personService;

        public RepositoryServiceTest()
        {
            //    this.countryService = countryService;
            this.personService = new PersonService(new PersonRepository(new Data.UnitOfWork()));
            //    this.menuService = menuService;
        }

        [TestMethod]
        public void IncludeTest()
        {
            var person = personService.GetFiltered(a => a.FirstName == "Tuğrul" && a.Country.Name == "Turkey", b => b.Country).FirstOrDefault();
        }
    }
}
