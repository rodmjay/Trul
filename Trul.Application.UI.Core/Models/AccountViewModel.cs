using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Trul.Application.DTO;

namespace Trul.Application.UI.Core.Models
{
    [Serializable]
    public class AccountViewModel : ViewModelBase
    {
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceName = "AccountVMRequiredUserName", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceName = "AccountVMPassword", ErrorMessageResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
    }
}
