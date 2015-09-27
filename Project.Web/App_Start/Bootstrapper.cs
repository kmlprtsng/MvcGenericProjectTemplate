using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Project.Data.Infrastructure;
using Project.Web.Mappings;

namespace Project.Web
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            //            builder.RegisterAssemblyTypes(typeof(ADD_REPOSITORY_CLASS_NAME_HERE).Assembly)
            //                .Where(t => t.Name.EndsWith("Repository"))
            //                .AsImplementedInterfaces().InstancePerRequest();
            // Services
            //            builder.RegisterAssemblyTypes(typeof(ADD_SERVICE_CLASS_NAME_HERE).Assembly)
            //               .Where(t => t.Name.EndsWith("Service"))
            //               .AsImplementedInterfaces().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}