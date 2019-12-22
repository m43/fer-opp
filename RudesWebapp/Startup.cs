using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RudesWebapp.Data;
using RudesWebapp.Models;

namespace RudesWebapp
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
            services.AddControllersWithViews();

           services.AddDbContext<RudesDatabaseContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("RudesDatabase")));

            services.AddDefaultIdentity<User>(
                   options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RudesDatabaseContext>();

            // using Microsoft.AspNetCore.Identity.UI.Services;
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                RudesDatabaseSeeder.Initialize(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SetupUserRoles(services).Wait();
        }

        // Hardcode user roles here if necessary
        private async Task SetupUserRoles(IServiceProvider serviceProvider)
        {

            Dictionary<string, string[]> userRoles = new Dictionary<string, string[]>()
            {
                { "Admin", new string[] { "mail@hivemind.org" } },
                { "Board", new string[] { } },
                { "Coach", new string[] { } },
                { "User", new string[] { } }
            };

            foreach (KeyValuePair<string, string[]> userRole in userRoles)
            {
                string roleName = userRole.Key;
                string[] usersInRole = userRole.Value;

                await SetupRole(roleName, usersInRole, serviceProvider);
            }

        }

        private async Task SetupRole(string roleName, string[] userIDs, IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            IdentityResult roleResult;
            var roleCheck = await RoleManager.RoleExistsAsync(roleName);
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            }

            foreach (string userID in userIDs)
            {
                User user = await UserManager.FindByEmailAsync(userID);
                await UserManager.AddToRoleAsync(user, roleName);
            }

        }

    }

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}