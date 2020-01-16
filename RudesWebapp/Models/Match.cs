using System;

namespace RudesWebapp.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string City { get; set; }
        public string SportsHall { get; set; }
        public string Country { get; set; }
        
    }
}