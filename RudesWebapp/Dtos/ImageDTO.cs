using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;
namespace RudesWebapp.Dtos
{
    
        public class ImageDTO
        {

            public int Id { get; set; }

            // Use only [Required] to show the default validation message for required attributes
            [Required(ErrorMessage = "It's necessary to specify the image name")]
            [Display(Name = "Image name", Prompt = "Enter the image name")]
            public string Name { get; set; }

            [Required(ErrorMessage = "It's necessary to specify the original name")]
            [Display(Name = "Original name", Prompt = "Enter the original name of the image")]
            public string OriginalName { get; set; }

            [Required(ErrorMessage = "It's necessary to specify the caption")]
            [Display(Name = "Post caption", Prompt = "Enter the caption")]
            public string Caption { get; set; }

            [Required(ErrorMessage = "It's necessary to specify the alt text")]
            [Display(Name = "Alt text", Prompt = "Enter the alt text")]
            public string AltText { get; set; }

        

        }
    }

