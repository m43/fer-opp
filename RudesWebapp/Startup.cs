using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;
using RudesWebapp.Services;
using RudesWebapp.Triggers;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Middleware;
using SixLabors.ImageSharp.Web.Processors;
using SixLabors.ImageSharp.Web.Providers;
using SixLabors.Memory;

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
                    options => options.SignIn.RequireConfirmedAccount = false) // No IEmailSender implemented
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

            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();

            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));

            services.AddImageSharpCore(
                    options =>
                    {
                        options.Configuration = SixLabors.ImageSharp.Configuration.Default;
                        options.MaxBrowserCacheDays = 7;
                        options.MaxCacheDays = 365;
                        options.CachedNameLength = 8;
                        options.OnParseCommands = (Action<ImageCommandContext>) (c =>
                        {
                            if (c.Commands.Count == 0)
                                return;
                            int width = (int) c.Parser.ParseValue<uint>(
                                c.Commands.GetValueOrDefault<string, string>("width"));
                            uint height = c.Parser.ParseValue<uint>(
                                c.Commands.GetValueOrDefault<string, string>("height"));
                            if ((uint) width > 4000U && height > 4000U)
                            {
                                c.Commands.Remove("width");
                                c.Commands.Remove("height");
                            }

                            if (height == 0)
                            {
                                width = ImageService.SanitizeSize(width);
                            }
                            else
                            {
                                width = 0;
                                height = (uint) ImageService.SanitizeSize((int) height);
                            }

                            c.Commands.Remove("width");
                            c.Commands.Remove("height");
                            c.Commands.Add("height", height.ToString());
                            c.Commands.Add("width", width.ToString());
                        });
                        options.OnBeforeSave = _ => { };
                        options.OnProcessed = _ => { };
                        options.OnPrepareResponse = _ => { };
                    })
                .SetRequestParser<QueryCollectionRequestParser>()
                .SetMemoryAllocator(provider => ArrayPoolMemoryAllocator.CreateWithMinimalPooling())
                .Configure<PhysicalFileSystemCacheOptions>(options => { options.CacheFolder = "different-cache"; })
                .SetCache<PhysicalFileSystemCache>()
                .SetCacheHash<CacheHash>()
                .AddProvider<PhysicalFileSystemProvider>()
                .AddProcessor<ResizeWebProcessor>()
                .AddProcessor<FormatWebProcessor>();

            services.TryAddEnumerable(new[] {ServiceDescriptor.Transient<ITrigger, CreateShoppingCartTrigger>()});
            // services.TryAddEnumerable(new[] {ServiceDescriptor.Transient<ITrigger, UpdateArticleAvailabilityTrigger>()}); // TODO

            AddServiceLayerToServices(services);
        }

        private static void AddServiceLayerToServices(IServiceCollection services)
        {
            services.AddScoped<ArticleInStoreService>();
            services.AddScoped<ArticleService>();
            services.AddScoped<ImageService>();
            services.AddScoped<ItemService>();
            services.AddScoped<ShoppingCartService>();
            services.AddScoped<UserService>();
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
            app.UseImageSharp();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("api","api/{controller}/{action}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}