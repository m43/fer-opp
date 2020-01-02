using System;

namespace RudesWebapp.Models
{
    public partial class Match : IDateCreated
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string City { get; set; }
        public string SportsHall { get; set; }
        public string Country { get; set; }
    }
}