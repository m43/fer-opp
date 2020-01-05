using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;

namespace RudesWebapp.Dtos
{
    public class PlayerDTO
    {
        public int Id { get; set; }

        
        // Note: This atribute is required anyways, so the below line is only needed if custom error message is anticipated
        //[Required(ErrorMessage = "It's necessary to specify the player's name")]
        [Display(Name = "Player Name", Prompt = "Enter the name of the player")]
        public string Name { get; set; }

        // [Required(ErrorMessage = "It's necessary to specify the player's last name")]
        public string LastName { get; set; }

        // [Required(ErrorMessage = "It's necessary to specify the date of birth")]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

        //[Required(ErrorMessage = "It's necessary to specify the player type")]
        public PlayerType PlayerType { get; set; }

        //[Required(ErrorMessage = "It's necessary to specify the player's position")]
        [Display(Name = "Player's basketball position")]
        public PlayerPositionType Position { get; set; }

        //[Required(ErrorMessage = "It's necessary to specify the image")]
        [Display(Name = "Player image")]
        public int? ImageId { get; set; }
    }
}