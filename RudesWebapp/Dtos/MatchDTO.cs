using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.ValidationAttributes;

namespace RudesWebapp.Dtos
{
    public class MatchDTO
    {
        public int Id { get; set; }

        // Use only [Required] to show the default validation message for required attributes
        [Required(ErrorMessage = "It's necessary to specify the name of the home team")]
        [Display(Name = "Home team", Prompt = "Enter the name of the home team")]
        public string HomeTeam { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the name of the away team")]
        [Display(Name = "Away team", Prompt = "Enter the name of the away team")]
        public string AwayTeam { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the city")]
        [Display(Name = "City", Prompt = "Enter the city where the match is played")]
        public string City { get; set; }

        [Display(Name = "Sport's hall", Prompt = "Enter the sport's hall where the match is played")]
        public string SportsHall { get; set; }

        [Display(Name = "Country", Prompt = "Enter the name of the country where the game is played")]
        public string Country { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the time of the match")]
        [Display(Name = "Time of match")]
        [DataType(DataType.DateTime)]
        [SqlDateTimeFormat]
        public DateTime? Time { get; set; }
    }
}