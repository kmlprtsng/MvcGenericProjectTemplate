﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Project.Data;
using Project.Domain.Entities;
using Project.Web.Infrastructure;

namespace Project.Web.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var dbContext = context.Get<ProjectContext>();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext))
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
            manager.EmailService = new SmtpEmailService(); //TODO Dependency Inject?
            return manager;
        }
    }
}