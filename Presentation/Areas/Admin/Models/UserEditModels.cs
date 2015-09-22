using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        [Display(Name = "Roles")]
        public RolesEditModel Roles { get; set; }
    }

    public class RolesEditModel
    {
        public List<string> RoleIds { get; set; }
        public List<SelectListItem> AllRoles { get; set; }

        public string SelectedRoleNames { get; set; }
    }
}