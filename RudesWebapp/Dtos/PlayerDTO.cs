using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;
using RudesWebapp.ValidationAttributes;

namespace RudesWebapp.Dtos
{
    public class PlayerDTO
    {
        public int Id { get; set; }

        // Use only [Required] to show the default validation message for required attributes
        [Required(ErrorMessage = "It's necessary to specify the player's name")]
        [Display(Name = "Player Name", Prompt = "Enter the name of the player")]
        public string Name { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the player's last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [SqlDateTimeFormat]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the player type")]
        public PlayerType PlayerType { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the player's position")]
        [Display(Name = "Player's basketball position")]
        public PlayerPositionType Position { get; set; }

        // [Required(ErrorMessage = "It's necessary to specify the image")]
        [Display(Name = "Player image")] public int? ImageId { get; set; }

        // TODO validate that image exists (if not null)
    }
}