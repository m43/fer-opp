using RudesWebapp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RudesWebapp.Dtos
{
    public class DiscountDTO
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the start date.")]
        [Display(Name = "Discount start date")]
        [DataType(DataType.Date)]
        [SqlDateTimeFormat]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the end date.")]
        [Display(Name = "Discount end date")]
        [DataType(DataType.Date)]
        [SqlDateTimeFormat]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "It's necessary to specify the discount percentage.")]
        [Display(Name = "Discount percentage")]
        [Range(0, 100, ErrorMessage = "Can only be between 0 .. 100")]
        // [DataType(DataType.)]  Kako specificirati da je double?
        public int? Percentage { get; set; }
    }
}
