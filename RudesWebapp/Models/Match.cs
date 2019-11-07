using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Match
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string City { get; set; }
        public string SportsHall { get; set; }
        public string Country { get; set; }
    }
}
