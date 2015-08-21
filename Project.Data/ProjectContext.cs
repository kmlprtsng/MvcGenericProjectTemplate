using Project.Data.Configuration;
using System.Data.Entity;
using Project.Model.Models;

namespace Project.Data
{
    public class ProjectContext : DbContext
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
            modelBuilder.Configurations.Add(new GadgetConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
        }
    }
}
