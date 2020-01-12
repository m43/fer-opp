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

        [Display(Name = "Start of post")] // TODO better name
        [DataType(DataType.Date)]
        [SqlDateTimeFormat]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End of post")] // TODO better name
        [DataType(DataType.Date)]
        [SqlDateTimeFormat]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Post image")] public int? ImageId { get; set; }

        // TODO additional validation - images, delta(Start, End)
    }
}