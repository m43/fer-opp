using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;
namespace RudesWebapp.Dtos
{
    public class OrderArticleDTO
    {
        public int OrderId { get; set; }

        public int ArticleId { get; set; }

        [Display(Name = "Quantity", Prompt = "Enter the quantity")] 
        [Range(1, int.MaxValue, ErrorMessage = "Must be at least 1")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the size")]
        [Display(Name = "Size", Prompt = "Choose between S,M,L,XL,XXL")] 
        [RegularExpression("S|M|L|XL|XXL")] //XS?
        public string Size { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the purchase price")]
        [Display(Name = "Purchase price", Prompt = "Enter the purchase price")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Must be at least 0")]
        public decimal? PurchasePrice { get; set; }

        
        [Display(Name = "Purchase discount", Prompt = "Enter the purchase discount")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Must be at least 0")]
        public decimal? PurchaseDiscount { get; set; }

    }
}

