using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    [Table("Documents")]
    public class Document
    {
        public int Id { get; set; }
        public string Type { get; set; } = String.Empty;
        public DateTime Date { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;

        public List<DocumentItem> Items { get; set; } = new List<DocumentItem>();
    }
}