using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;
using RudesWebapp.ValidationAttributes;

namespace RudesWebapp.Dtos
{
    public class PostDTO
    {
        
            public int Id { get; set; }

            // Use only [Required] to show the default validation message for required attributes
            [Required(ErrorMessage = "It's necessary to specify the post's title")]
            [Display(Name = "Post title", Prompt = "Enter the post title")]
            public string Title { get; set; }

            [Required(ErrorMessage = "It's necessary to specify the content")]
            [Display(Name = "Post content", Prompt = "Enter the content")] 
            public string Content { get; set; }

            [Display(Name = "Post image")] public int? ImageId { get; set; }

            [Required(ErrorMessage = "It's necessary to specify the post type")]
            public string PostType { get; set; }

            [Required(ErrorMessage = "It's necessary to specify the start date")]
            [Display(Name = "Start Date")]
            [DataType(DataType.Date)]
            [SqlDateTimeFormat]
            public DateTime StartDate { get; set; }

            [Required(ErrorMessage = "It's necessary to specify the end date")]
            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            [SqlDateTimeFormat]
            public DateTime EndDate { get; set; }

    }
    }

