using System.Data.Entity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WilliamsWeb1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual ICollection<Project> MyProjects { get; set; }
        public string PaymentMethod { get; private set; } = "credit";
        public virtual ICollection<Chats> Chats { get; set; }
        public string Company { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("mydb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WilliamsWeb1.Models.Project> Projects { get; set; }
        public System.Data.Entity.DbSet<WilliamsWeb1.Models.Chats> Chats { get; set; }
        public System.Data.Entity.DbSet<WilliamsWeb1.Models.Message> Messages { get; set; }
        public System.Data.Entity.DbSet<WilliamsWeb1.Models.Milestone> Milestones { get; set; }

    }
}
