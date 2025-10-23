using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Payloads
{
    public class DocumentItemPayload
    {
        [Required(ErrorMessage = "Ordinal is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Ordinal must be at least 1 and cannot be negative")]
        public int Ordinal { get; set; }
        [Required(ErrorMessage = "Product is required")]
        public string Product { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1 and cannot be negative")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.00, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Tax rate is required")]
        [Range(0, 100, ErrorMessage = "Tax rate must be between 0 and 100")]
        public int TaxRate { get; set; }
    }
}