using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Application.DTO;

namespace Trul.Application.UI.Core.Models
{
    [Serializable]
    public class CountryViewModel : ViewModelBase
    {
        public CountryViewModel()
        {
            Countries = Enumerable.Empty<CountryDTO>();
        }

        public IEnumerable<CountryDTO> Countries { get; set; }
    }
}