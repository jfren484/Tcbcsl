using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class AdminListTableModel
    {
        public string ListItemName { get; set; }
        public string CreateItemUrl { get; set; }
    }

    public abstract class EditModelBase
    {
        [Display(Name = "History")]
        public AuditDetailsModel AuditDetails { get; set; }

        [Display(Name = "")]
        public string EditUrl { get; set; }
    }
}