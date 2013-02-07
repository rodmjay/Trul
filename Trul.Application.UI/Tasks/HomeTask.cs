using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.DTO;
using Trul.Application.UI.Core.Models;
using Trul.Application.UI.Core.Tasks;
using Trul.Service.Core;

namespace Trul.Application.UI.Tasks
{
    public class HomeTask : TaskBase, IHomeTask
    {
        private ICountryService countryService;
        private IMenuService menuService;
        private IPersonService personService;
        private IUserService userService;

        public HomeTask(ICountryService countryService, IPersonService personService, IMenuService menuService, IUserService userService)
        {
            this.countryService = countryService;
            this.personService = personService;
            this.menuService = menuService;
            this.userService = userService;
        }

        public HomeViewModel Index()
        {
            var homeViewModel = new HomeViewModel();

            var aa = countryService.Get(1);

            var persons = personService.GetNameLike("tuğ");

            var menu = new MenuDTO();
            menu.ApplicationCode = "code 1";
            menu.LinkName = "link 1";
            //menuService.Add(menu);
            //var menus = menuService.GetAll();
            //menuService.Delete(menus.First());

            var tempPerson = new PersonDTO();
            tempPerson.FirstName = "ali";
            tempPerson.LastName = "veli";
            tempPerson.Country = countryService.GetFiltered(a => a.Name == "Turkey").First();
            //personService.Add(tempPerson);



            var person = personService.GetFiltered(a => a.FirstName == "Tuğrul", b => b.Country).FirstOrDefault();
            //person.FirstName = "Tuğrul";
            //person.Country = countryService.GetFiltered(a => a.ID == 2).First();
            //personService.Update(person);
            homeViewModel.FirstName = person.FirstName;
            homeViewModel.LastName = person.LastName;
            homeViewModel.Countries = countryService.GetAll();
            return homeViewModel;
        }
    }
}
