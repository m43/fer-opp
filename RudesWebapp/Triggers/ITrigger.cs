using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RudesWebapp.Triggers
{
    public interface ITrigger
    {
        void RegisterChangedEntities(ChangeTracker changeTracker);
        Task TriggerAsync();
    }
}