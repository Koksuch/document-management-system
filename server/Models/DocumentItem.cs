using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("DocumentItems")]
    public class DocumentItem
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int Ordinal { get; set; }
        public string Product { get; set; } = String.Empty;
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int TaxRate { get; set; }

        public Document? Document { get; set; }
    }
}