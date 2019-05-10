using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Tcbcsl.Data.Entities;

namespace Tcbcsl.Data.Identity
{
    public class TcbcslUser : IdentityUser
    {
        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        public virtual ICollection<Team> ManagedTeams { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        // TODO: remove or fix
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TcbcslUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }
}
