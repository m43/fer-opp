using System.ComponentModel.DataAnnotations;

namespace RudesWebapp.Dtos
{
    public class ShoppingCartArticleDTO
    {
        public int ShoppingCartId { get; set; }
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the quantity.")]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the size.")]
        [Display(Name = "Size")]
        [RegularExpression("S|M|L|XL|XXL")]
        public string Size { get; set; }
    }
}