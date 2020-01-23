using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using RudesWebapp.Data;
using RudesWebapp.Models;

namespace RudesWebapp.Triggers
{
    public class CreateShoppingCartTrigger : TriggerBase<User>
    {
        private readonly ILogger<CreateShoppingCartTrigger> _logger;
        private readonly RudesDatabaseContext _context;

        public CreateShoppingCartTrigger(ILogger<CreateShoppingCartTrigger> logger, RudesDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        protected override IEnumerable<TriggerEntityVersion<User>> RegisterChangedEntitiesInternal(
            ChangeTracker changeTracker)
        {
            var users = changeTracker
                .Entries<User>()
                .Where(entry => entry.State == EntityState.Added);

            var entityEntries = users as EntityEntry<User>[] ?? users.ToArray();
            foreach (var userEntity in entityEntries)
            {
                var shoppingCart = new ShoppingCart
                {
                    User = userEntity.Entity
                };
                _context.ShoppingCart.Add(shoppingCart);

                userEntity.Entity.ShoppingCart = shoppingCart;
            }

            return entityEntries.Select(TriggerEntityVersion<User>.CreateFromEntityProperty);
        }

        protected override Task TriggerAsyncInternal(TriggerEntityVersion<User> trackedTriggerEntity)
        {
            _logger.LogInformation($"Shopping cart created for user {trackedTriggerEntity.New.Id}");
            return Task.CompletedTask;
        }
    }
}