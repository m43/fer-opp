using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;

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

        // TODO how to color
        // [JsonConverter(typeof(ColorConverter))]
        public Color Color { get; set; }

        public int? ImageId { get; set; }
    }
}