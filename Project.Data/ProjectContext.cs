using Microsoft.AspNet.Identity.EntityFramework;
using Project.Data.Configuration;
using System.Data.Entity;
using Project.Domain.Entities;

namespace Project.Data
{
    public class ProjectContext : IdentityDbContext<ApplicationUser>
    {
        public ProjectContext() : base("ProjectContext") { }

        public DbSet<Gadget> Gadgets { get; set; }
        public DbSet<Category> Categories { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new GadgetConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
        }

        public static ProjectContext Create()
        {
            return new ProjectContext();
        }
    }
}
