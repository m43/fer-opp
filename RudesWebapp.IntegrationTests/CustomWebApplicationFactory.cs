using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using RudesWebapp.Data;

namespace RudesWebapp.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove RudesDatabaseContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<RudesDatabaseContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a database context (ApplicationDbContext) using an in-memory 
                // database for testing.
                services.AddDbContext<RudesDatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("ma db")
                        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<RudesDatabaseContext>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    RudesDatabaseSeeder.Initialize(scope);
                }
            });
        }
    }
}