/*using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnackisWebApp.Areas.Identity.Data;
using SnackisWebApp.Models;

[assembly: HostingStartup(typeof(SnackisWebApp.Areas.Identity.IdentityHostingStartup))]
namespace SnackisWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SnackisUserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SnackisUserContextConnection")));

                services.AddDefaultIdentity<SnackisUser>(options =>
                    {
                        // Password settings
                        options.Password.RequireDigit = default;
                        options.Password.RequireLowercase = default;
                        options.Password.RequireNonAlphanumeric = default;
                        options.Password.RequireUppercase = default;
                        options.Password.RequiredLength = 8;
                        options.Password.RequiredUniqueChars = default;

                        // Sign in settings
                        options.SignIn.RequireConfirmedAccount = default;
                        options.SignIn.RequireConfirmedEmail = default;
                        options.SignIn.RequireConfirmedPhoneNumber = default;

                        // Lockout settings
                        options.Lockout.AllowedForNewUsers = default;
                        options.Lockout.DefaultLockoutTimeSpan = default;
                        options.Lockout.MaxFailedAccessAttempts = default;

                        // User settings
                        options.User.RequireUniqueEmail = default;
                        options.User.AllowedUserNameCharacters = default;
                    })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<SnackisUserContext>();

                services.AddIdentity<SnackisUser, IdentityRole>();
            });
        }
    }
}*/