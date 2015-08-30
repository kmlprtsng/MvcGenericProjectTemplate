using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Project.Domain.Entities;

namespace Project.Data
{
    public class ProjectContext : IdentityDbContext<ApplicationUser>
    {
        public ProjectContext() : base("ProjectContext") { }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static ProjectContext Create()
        {
            return new ProjectContext();
        }
    }
}
