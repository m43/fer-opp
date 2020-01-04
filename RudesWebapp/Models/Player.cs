using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RudesWebapp.Models
{
    public partial class Player : IDateCreatedAndUpdated
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public PlayerType PlayerType { get; set; }

        public PlayerPositionType Position { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlayerType
    {
        Seniors,
        Juniors,
        Cadets,
        YoungCadets,
        SportSchools,
        MiniBasketball
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlayerPositionType
    {
        PG, // PointGuard
        SG, // ShootingGuard
        SF, // SmallForward
        PF, // PowerForward
        C // Center
    }
}