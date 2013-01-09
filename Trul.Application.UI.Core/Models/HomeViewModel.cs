using System;
using System.Collections.Generic;
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public IEnumerable<CountryDTO> Countries { get; set; }

        public CountryDTO SelectedCountry { get; set; }
    }
}
