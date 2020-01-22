using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace RudesWebapp.Dtos
{
    public class ItemDTO
    {
        // TODO Constructor - not sure how to use the AutoMapper in this case

        public int ShoppingCartId { get; set; }
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the quantity.")]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the size.")]
        [Display(Name = "Size")]
        [RegularExpression("S|XS|M|L|XL|XXL|U")] // TODO ex. a bottle does not have S/M/L.. size. Maybe U like universal?
        public string Size { get; set; }

        [Required] public string Type { get; set; }

        [Required]
        [Range(0, 9999999999999999.99, ErrorMessage = "Only positive integers allowed")]
        [RegularExpression(@"^\d+(\.\d{0,2})?$")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "{0} must be greater or equal to {1} characters in length")]
        public string Name { get; set; }

        [Required] public string Description { get; set; }

        // TODO image validation
        public int? ImageId { get; set; }

        // TODO not sure what to do about this
        public int? Argb
        {
            get => ArticleColor?.ToArgb();
            set => ArticleColor = value == null ? (Color?) null : Color.FromArgb(value.Value);
        }

        [NotMapped] public Color? ArticleColor { get; set; }

        [Required] [Range(0, 99)] public int Percentage { get; set; }
    }
}