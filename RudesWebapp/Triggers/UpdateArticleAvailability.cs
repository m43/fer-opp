using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using RudesWebapp.Data;
using RudesWebapp.Models;
using Z.EntityFramework.Plus;

namespace RudesWebapp.Triggers
{
    public class UpdateArticleAvailabilityTrigger : TriggerBase<ArticleAvailability>
    {
        private readonly ILogger<UpdateArticleAvailabilityTrigger> _logger;
        private readonly RudesDatabaseContext _context;

        public UpdateArticleAvailabilityTrigger(ILogger<UpdateArticleAvailabilityTrigger> logger,
            RudesDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        protected override IEnumerable<TriggerEntityVersion<ArticleAvailability>> RegisterChangedEntitiesInternal(
            ChangeTracker changeTracker)
        {
            var availabilities = changeTracker
                .Entries<ArticleAvailability>()
                .Where(entry => entry.State == EntityState.Modified);

            var entityEntries = availabilities as EntityEntry<ArticleAvailability>[] ?? availabilities.ToArray();
            foreach (var availability in entityEntries)
            {
                if (availability.Entity.Quantity == 0)
                {
                    // TODO delete the entity
                }
            }

            return entityEntries.Select(TriggerEntityVersion<ArticleAvailability>.CreateFromEntityProperty);
        }

        protected override Task TriggerAsyncInternal(TriggerEntityVersion<ArticleAvailability> trackedTriggerEntity)
        {
            _logger.LogInformation($"Deleted article availabilities with 0 quantity");
            return Task.CompletedTask;
        }
    }
}