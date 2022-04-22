using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrondEnd.Shared.Models
{
    public class Products
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
