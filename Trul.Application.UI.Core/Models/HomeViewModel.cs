using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Trul.Application.DTO;

namespace Trul.Application.UI.Core.Models
{
    [Serializable]
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            Countries = Enumerable.Empty<CountryDTO>();
        }

        [Display(Name="FirstName", ResourceType=typeof(Resources.Resource))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        public string LastName { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Resources.Resource))]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Display(Name = "Country", ResourceType = typeof(Resources.Resource))]
        public IEnumerable<CountryDTO> Countries { get; set; }

        [Display(Name = "SelectedCountry", ResourceType = typeof(Resources.Resource))]
        public CountryDTO SelectedCountry { get; set; }
    }
}
