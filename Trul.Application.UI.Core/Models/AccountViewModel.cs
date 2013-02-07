using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Trul.Application.DTO;
using Trul.Framework.Rules;
using Trul.Framework.Rules.SyntaxHelpers;

namespace Trul.Application.UI.Core.Models
{
    [Serializable]
    public class AccountViewModel : ViewModelBase
    {
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [Display(Name = "PasswordAgain", ResourceType = typeof(Resources.Resource))]
        public string PasswordAgain { get; set; }

        public bool IsRememberMe { get; set; }

        public override IEnumerable<IRulesGroup> ValidatorRules()
        {
            return new List<IRulesGroup>
                       {
                           DefineValidator.For<AccountViewModel>()

                               .WhereProperty(p => p.UserName).SatisfiedAs(Should.NotBeNullOrEmpty)
                                    .WithReason(Resources.Resource.AccountVMRequiredUserName).AsError()
                               //.WhereProperty(p => p.Password).SatisfiedAs(Should.NotBeLongerThan(10))
                                   // .WithReason("Слишком длинное имя")

                               .WhereProperty(p => p.Password).SatisfiedAs(Should.NotBeNullOrEmpty)
                                    .WithReason(Resources.Resource.AccountVMPassword).AsError()

                               .WhereProperty(p => p.PasswordAgain, p=>p.Password).SatisfiedAs(Should.EqualTo)
                                    .WithReason(Resources.Resource.AccountVMPasswordAgain).AsError()
                       };
        }
    }
}
