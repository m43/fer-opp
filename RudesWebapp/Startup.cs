using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RudesDatabaseContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("RudesDatabase"));
                    options.EnableSensitiveDataLogging(); // TODO remove for production
                }
            );
            services.AddDefaultIdentity<User>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RudesDatabaseContext>();
            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(options =>
                {
                    var googleAuthNSection = Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                // RudesDatabaseSeeder.Initialize(app); // uncomment to seed the database
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
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