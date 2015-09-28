using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class UserEditModel : EditModelBase
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Roles")]
        public RolesEditModel Roles { get; set; }

        [Display(Name = "Assigned Teams")]
        public AssignedTeamsEditModel AssignedTeams { get; set; }
    }

    public class RolesEditModel
    {
        public List<string> RoleIds { get; set; }

        public List<SelectListItem> AllRoles { get; set; }

        public string SelectedRoleNames { get; set; }
    }

    public class AssignedTeamsEditModel
    {
        public List<int> TeamIds { get; set; }

        public List<SelectListItem> AllTeams { get; set; }

        public string SelectedTeamNames { get; set; }
    }
}