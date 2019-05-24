using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tcbcsl.Data.Entities;

namespace Tcbcsl.Data.Identity
{
    public class TcbcslUser : IdentityUser
    {
        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        public virtual ICollection<TcbcslUserTeam> TcbcslUserTeams { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
