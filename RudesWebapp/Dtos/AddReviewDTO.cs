using System.ComponentModel.DataAnnotations;

namespace RudesWebapp.Dtos
{
    public class AddReviewDTO
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Article to review")]
        public int ArticleId { get; set; }

        [Required]
        [Display(Name = "Article rating", Prompt = "Enter the rating")]
        [Range(0, 5, ErrorMessage = "Must be between 0 and 5")]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Comment", Prompt = "Enter the comment")]
        public string Comment { get; set; }
    }
}