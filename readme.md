# Introduction

There is an overhead starting a new N-tier project and the purpose of this repository is to solve that. 

This project is based on the ideas of [Christos S](http://en.gravatar.com/chsakell) and his article [ASP.NET MVC Solution Architecture – Best Practices](http://chsakell.com/2015/02/15/asp-net-mvc-solution-architecture-best-practices/).

## Getting Started
- Clone the project
- Right click on the Project.Web and set that as your default project.

## Renaming the Project

- Rename the solution
- Rename each project
- Go into the properties of each project and change the assembly name. (use Resharper or something similar to fix all the namespaces)
- Rename ProjectContext.cs and ProjectSeedData.cs class (in Data project)
- (optional) Change connection string name
	- Rename ConnectionString name in ProjectContext.cs class (may have renamed the class by this point)
	- Update web.config in the web project.
- More instruction coming soon.

## Add Domain Entities
Add domain entities as required.

## Add Repositories
- Add the domain entities to the context 

`public DbSet<Category> Categories { get; set; }`

- Create domain entity configuration class

```
public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("Categories");
            Property(c => c.Name).IsRequired().HasMaxLength(50);
        }
    }
```

-  update `onModelCreating` method in the context with configurations

`modelBuilder.Configurations.Add(new CategoryConfiguration());`

- Add any required data seed to the ProductSeedData.cs (possibly renamed) file.
- Create Repository class
```
public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Category GetCategoryByName(string categoryName)
        {
            var category = this.DbContext.Categories.Where(c => c.Name == categoryName).FirstOrDefault();

            return category;
        }

        public override void Update(Category entity)
        {
            entity.DateUpdated = DateTime.Now;
            base.Update(entity);
        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByName(string categoryName);
    }
```

## Add Services
instructions coming soon....

## Setup AutofacContainer
instructions coming soon....

## Domain to ViewModel Mapping
instructions coming soon....

## ViewModel to Domain Mapping
instructions coming soon....

## TODO
- Add the web project with Form based authentication
- Look into setting up WebApi with Token based authentication

## Troubleshooting
- If you get any dll errors, try cleaning the solution, check the bin folder for any old dlls and manually delete if required. Build and Try again.