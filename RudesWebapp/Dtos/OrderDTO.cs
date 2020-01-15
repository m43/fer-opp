using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RudesWebapp.Dtos
{
    public class OrderDTO
    {
        public string Address { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public IEnumerable<ItemDTO> Items { get; set; }
    }
}
