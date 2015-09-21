using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class UserEditModel : EditModelBase
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        [UIHint("TextSingleLine")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        [UIHint("TextSingleLine")]
        public string Email { get; set; }

        public string RoleList { get; set; }

        [Display(Name = "Roles")]
        public List<string> RoleIds { get; set; }
    }
}