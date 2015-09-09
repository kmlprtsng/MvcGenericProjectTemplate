using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Project.Data;
using Project.Domain.Entities;

namespace Project.Web.Infrastructure
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IIdentityMessageService emailService)
            : base(store)
        {
            EmailService = emailService;
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var dbContext = context.Get<ProjectContext>();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext), new SmtpEmailService()) //TODO-KC Dependency Inject
            {
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 8,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false
                }
            };

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.UserLockoutEnabledByDefault = SettingsManager.UserLockoutEnabledByDefault;
            manager.DefaultAccountLockoutTimeSpan = SettingsManager.DefaultAccountLockoutTimeSpan;
            manager.MaxFailedAccessAttemptsBeforeLockout = SettingsManager.MaxFailedAccessAttemptsBeforeLockout;
            manager.UserTokenProvider = new EmailTokenProvider<ApplicationUser, string>();
            return manager;
        }
    }
}