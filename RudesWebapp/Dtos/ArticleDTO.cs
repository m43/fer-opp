using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using RudesWebapp.ValidationAttributes;

namespace RudesWebapp.Dtos
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "{0} must be greater or equal to {1} characters in length")]
        public string Name { get; set; }

        [Required] public string Description { get; set; }

        [Required] public string Type { get; set; }

        [Required]
        [Range(0, 9999999999999999.99, ErrorMessage = "Only positive integers allowed")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [HexColor]
        [StringLength(7)]
        [Display(Name = "Color (in hex format like #F1C40F")]
        public string? Color
        {
            get => ArticleColor != null ? ColorTranslator.ToHtml(ArticleColor.Value) : null;
            set => ArticleColor = value == null ? (Color?) null : ColorTranslator.FromHtml(value);
        }

        [NotMapped] private Color? ArticleColor { get; set; }

        public int? ImageId { get; set; }

        // TODO validate that image exists (if not null)
    }
}