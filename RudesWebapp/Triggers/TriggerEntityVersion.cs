using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RudesWebapp.Triggers
{
    public class TriggerEntityVersion<T>
    {
        public T Old { get; set; }
        public T New { get; set; }

        public static TriggerEntityVersion<TResult> CreateFromEntityProperty<TResult>(EntityEntry<TResult> entry)
            where TResult : class, new()
        {
            TriggerEntityVersion<TResult> returnedResult = new TriggerEntityVersion<TResult>
            {
                New = new TResult(),
                Old = new TResult()
            };

            foreach (PropertyInfo propertyInfo in typeof(TResult)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Where(pi => entry.OriginalValues.Properties.Any(property => property.Name == pi.Name)))
            {
                if (propertyInfo.CanRead &&
                    (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType.IsValueType))
                {
                    propertyInfo.SetValue(returnedResult.Old, entry.OriginalValues[propertyInfo.Name]);
                }
            }

            foreach (PropertyInfo propertyInfo in typeof(TResult)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Where(pi => entry.OriginalValues.Properties.Any(property => property.Name == pi.Name)))
            {
                if (propertyInfo.CanRead &&
                    (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType.IsValueType))
                {
                    propertyInfo.SetValue(returnedResult.New, entry.CurrentValues[propertyInfo.Name]);
                }
            }

            return returnedResult;
        }
    }
}