using System.ComponentModel.DataAnnotations;

namespace RudesWebapp.Dtos
{
    public class EditOrderDTO
    {
        [Required] public int Id { get; set; }

        public bool Fulfilled { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the caption")]
        [Display(Name = "Order address", Prompt = "Enter the order address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the city name")]
        [Display(Name = "City name", Prompt = "Enter the name of the city")]
        public string City { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the postal code")]
        [Display(Name = "PostalCode", Prompt = "Enter the postal code")]
        public int? PostalCode { get; set; }
    }
}