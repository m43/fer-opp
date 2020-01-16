using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RudesWebapp.Dtos
{
    public class AddImageDTO
    {
        public int Id { get; set; }

        // // Use only [Required] to show the default validation message for required attributes
        // [Required(ErrorMessage = "It's necessary to specify the image name")]
        // [Display(Name = "Image name", Prompt = "Enter the image name")]
        // public string Name { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the title")]
        [Display(Name = "Title", Prompt = "Enter the title of the image")]
        [RegularExpression("^[a-zA-Z0-9\\s]+$", ErrorMessage = "Title can only have alphanumeric characters and spaces")]
        public string Title { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the caption")]
        [Display(Name = "Post caption", Prompt = "Enter the caption")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the alt text")]
        [Display(Name = "Alt text", Prompt = "Enter the alt text")]
        public string AltText { get; set; }

        public IFormFile Picture { get; set; }
    }
}