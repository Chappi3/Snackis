using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SnackisWebApp.Areas.Identity.Data;
using SnackisWebApp.Models;
using System;
using System.Threading.Tasks;
using SnackisWebApp.Enums;

namespace SnackisWebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SnackisUserContext>();
                    await context.Database.MigrateAsync();

                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userRoleExists = await roleManager.RoleExistsAsync(Roles.User.ToString());
                    var adminRoleExists = await roleManager.RoleExistsAsync(Roles.Admin.ToString());

                    if (!userRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
                    }
                    if (!adminRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                    }

                    var userManager = services.GetRequiredService<UserManager<SnackisUser>>();
                    var adminUser = await userManager.FindByNameAsync("Admin");

                    if (adminUser == null)
                    {
                        var admin = new SnackisUser
                        {
                            Email = "Admin@mail.com",
                            UserName = "Admin",
                            EmailConfirmed = true
                        };

                        await userManager.CreateAsync(admin, "AdminPassword1!");
                        await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
                    }

                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occurred adding admin or roles");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
