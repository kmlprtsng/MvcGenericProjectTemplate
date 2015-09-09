using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Project.Data.Infrastructure;
using Project.Web.Infrastructure;
using Project.Web.Mappings;

namespace Project.Web
{
    public static class Bootstrapper
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
            builder.RegisterType<SmtpEmailService>().As<IIdentityMessageService>().InstancePerRequest();

            // Repositories
            //            builder.RegisterAssemblyTypes(typeof(ADD_REPOSITORY_CLASS_NAME_HERE).Assembly)
//                .Where(t => t.Name.EndsWith("Repository"))
//                .AsImplementedInterfaces().InstancePerRequest();
            // Services
//            builder.RegisterAssemblyTypes(typeof(ADD_SERVICE_CLASS_NAME_HERE).Assembly)
//               .Where(t => t.Name.EndsWith("Service"))
//               .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}