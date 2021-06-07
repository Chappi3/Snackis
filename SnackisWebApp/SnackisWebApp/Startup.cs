using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnackisWebApp.Areas.Identity.Data;
using SnackisWebApp.Models;

namespace SnackisWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SnackisUserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SnackisUserContextConnection")));

            services.AddIdentity<SnackisUser, IdentityRole>(options =>
                {
                    // Password settings
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = default;
                    options.Password.RequireUppercase = default;
                    options.Password.RequireLowercase = default;
                    options.Password.RequiredUniqueChars = default;
                    options.Password.RequireNonAlphanumeric = default;

                    // Sign in settings
                    options.SignIn.RequireConfirmedEmail = default;
                    options.SignIn.RequireConfirmedAccount = default;
                    options.SignIn.RequireConfirmedPhoneNumber = default;

                    // Lockout settings
                    options.Lockout.AllowedForNewUsers = default;
                    options.Lockout.DefaultLockoutTimeSpan = default;
                    options.Lockout.MaxFailedAccessAttempts = default;

                    // User settings
                    options.User.RequireUniqueEmail = default;
                    options.User.AllowedUserNameCharacters = default;
                })
                .AddEntityFrameworkStores<SnackisUserContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", p => p.RequireRole("Admin"));
            });

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin", "RequireAdminRole");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
