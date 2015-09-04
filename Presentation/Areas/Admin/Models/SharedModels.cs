using System;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Presentation.Areas.Admin.Models
{
    public class AdminListTableModel
    {
        public string ListItemName { get; set; }
        public string CreateItemUrl { get; set; }
    }

    public class EditModelBase
    {
        [Display(Name = "History")]
        public AuditDetailsModel AuditDetails { get; set; }

        [Display(Name = "")]
        public string EditUrl { get; set; }
    }

    public class AuditDetailsModel
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}