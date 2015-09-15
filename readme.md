# Introduction

There is an overhead starting a new N-tier project and the purpose of this repository is to solve that. 

This project is based on the ideas of [Christos S](http://en.gravatar.com/chsakell) and his article [ASP.NET MVC Solution Architecture – Best Practices](http://chsakell.com/2015/02/15/asp-net-mvc-solution-architecture-best-practices/).

## Pre-requisites
- VS 2013 or above
- Bower is installed


## Getting Started
- Clone the project
- Right click on the Project.Web and set that as your default project.
- In the project seed a user is created by default with Logins: admin@yoursite.com\password!.
- Create "C:\TestMailMessages" folder. Emails will be sent to this folder. To send email via the SMTP server please configure the settings in the web.config.

## Bower Settings
The following two libraries have been installed
- Bootstrap
- Jquery

You might consider adding the following libraries for MVC 
- fontawesome
- jquery-ajax-unobtrusive

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
- Add service class for domain entity e.g.
```
public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(string name = null);
        Category GetCategory(int id);
        Category GetCategory(string name);
        void CreateCategory(Category category);
        void SaveCategory();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categorysRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(ICategoryRepository categorysRepository, IUnitOfWork unitOfWork)
        {
            this.categorysRepository = categorysRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ICategoryService Members

        public IEnumerable<Category> GetCategories(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return categorysRepository.GetAll();
            else
                return categorysRepository.GetAll().Where(c => c.Name == name);
        }

        public Category GetCategory(int id)
        {
            var category = categorysRepository.GetById(id);
            return category;
        }

        public Category GetCategory(string name)
        {
            var category = categorysRepository.GetCategoryByName(name);
            return category;
        }

        public void CreateCategory(Category category)
        {
            categorysRepository.Add(category);
        }

        public void SaveCategory()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
```

## Setup AutofacContainer (for dependency injection)
- Register your first **repository** class with Autofac in the Web project in the Bootstrapper class's **SetAutofacContainer** method. Required only once.

```
	builder.RegisterAssemblyTypes(typeof(CategoryRepository).Assembly)
		.Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces().InstancePerRequest();
```
- Register your first **service** class with Autofac in the Web project in the Bootstrapper class's **SetAutofacContainer** method. Required only once.

```
	builder.RegisterAssemblyTypes(typeof(CategoryService).Assembly)
    	.Where(t => t.Name.EndsWith("Service"))
		.AsImplementedInterfaces().InstancePerRequest();
```

## Setup AutoMapper
- Set up domain to ViewModel mappings in **DomainToViewModelMappingProfile** class e.g.
```
		protected override void Configure()
        {
            Mapper.CreateMap<Category,CategoryViewModel>();
        }
```
- Set up ViewModel to domain mapping in **ViewModelToDomainMappingProfile** class e.g.
```
        protected override void Configure()
        {
            Mapper.CreateMap<GadgetFormViewModel, Gadget>()
                .ForMember(g => g.Name, map => map.MapFrom(vm => vm.GadgetTitle))
                .ForMember(g => g.Description, map => map.MapFrom(vm => vm.GadgetDescription))
                .ForMember(g => g.Price, map => map.MapFrom(vm => vm.GadgetPrice))
                .ForMember(g => g.Image, map => map.MapFrom(vm => vm.File.FileName))
                .ForMember(g => g.CategoryID, map => map.MapFrom(vm => vm.GadgetCategory));
        }
```

## Configuring Security
If you need to configure the **password requirements** then go to the **ApplicationUserManager** class in the Web Project. Also in the web.config file (under the app settings), the **account lockout** feature can also be toggled on or off along with other relevant settings for that.

## Spam Filter
Spam filter has been added in order to stop the spammers from registering on the site. The implementation of it is from Daniel Palme's [blog post](http://www.palmmedia.de/Blog/2009/11/17/aspnet-mvc-how-to-protect-your-website-against-spam). Please refer to that for more information.

## Emails
Currently when the user registers, an email is sent to confirm the registration. The emails will be stored to the C:\TestMailMessages folder unless the web.config is updated with different settings. 

It might be useful to use the [MVCMailer](https://github.com/smsohan/MvcMailer) or something similar to create good looking email.

## TODO
- Hook up AngularJs Project
- Look into setting up WebApi with Token based authentication

## Troubleshooting
- If you get any dll errors, try cleaning the solution, check the bin folder for any old dlls and manually delete if required. Build and Try again.