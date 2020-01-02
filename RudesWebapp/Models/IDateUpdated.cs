using System;

namespace RudesWebapp.Models
{
    public interface IDateUpdated
    {
        DateTime? LastModificationDate { get; set; }
    }
}