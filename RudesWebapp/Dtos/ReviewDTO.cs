using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;
namespace RudesWebapp.Dtos
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        [Display(Name = "Article rating", Prompt = "Enter the rating")]
        [Range(0, 5, ErrorMessage = "Must be between 0 and 5")]
        public int Rating { get; set; }

        [Display(Name = "Comment", Prompt = "Enter the comment")]
        public string Comment { get; set; }

        public string UserId { get; set; }

        public bool Blocked { get; set; }

    }
}
