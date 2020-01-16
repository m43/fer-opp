using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.ValidationAttributes;

namespace RudesWebapp.Dtos
{
    public class PostDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the post's title")]
        [Display(Name = "Post title", Prompt = "Enter the post title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the content")]
        [Display(Name = "Post content", Prompt = "Enter the content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the post type")]
        public string PostType { get; set; }

        [Display(Name = "Date of post being active")]
        [DataType(DataType.Date)]
        [SqlDateTimeFormat]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Date of post being no more active")]
        [DataType(DataType.Date)]
        [SqlDateTimeFormat]
        [AttributeGreaterThan(nameof(StartDate), ErrorMessage = "The end date must be after the start date.")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Post image")] public int? ImageId { get; set; }

        [Display(Name = "Image")] public ImageDTO Image { get; set; }
    }
}