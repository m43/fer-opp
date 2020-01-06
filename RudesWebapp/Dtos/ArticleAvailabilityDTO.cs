using System;
using System.ComponentModel.DataAnnotations;
using RudesWebapp.Models;
namespace RudesWebapp.Dtos
{
    public class ArticleAvailability
    {
        //unsure if quantity is required TODO

        public int ArticleId { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the size")]
        [Display(Name = "Size", Prompt = "Choose between S,M,L,XL,XXL")] // XS?
        [RegularExpression("S|M|L|XL|XXL")] //XS?
        public string Size { get; set; }

        [Display(Name = "Quantity", Prompt = "Enter the quantity")] // XS?
        [Range(1, int.MaxValue, ErrorMessage = "Must be at least 1")]
        public int? Quantity { get; set; }
    }
}
