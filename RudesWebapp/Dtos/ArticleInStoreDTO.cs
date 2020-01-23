using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RudesWebapp.Dtos
{
    public class ArticleInStoreDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string? Color
        {
            get => ArticleColor != null ? ColorTranslator.ToHtml(ArticleColor.Value) : null;
            set => ArticleColor = value == null ? (Color?)null : ColorTranslator.FromHtml(value);
        }

        public Color? ArticleColor { get; set; }

        public int? ImageId { get; set; }

        
        // TODO validate that image exists (if not null)

        [Display(Name = "Image")] public ImageDTO Image { get; set; }
        
        public List<String> Sizes { get; set; }

        public List<int> Quantities { get; set; }
    }
}
