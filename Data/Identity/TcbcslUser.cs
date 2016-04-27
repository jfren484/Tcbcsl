using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tcbcsl.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Tcbcsl.Data.Identity
{
    public class TcbcslUser : IdentityUser
    {
        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        public virtual ICollection<Team> AssignedTeams { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TcbcslUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
