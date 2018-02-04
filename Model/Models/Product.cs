using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagerAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
            ApprovedByGeneralManager = false;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Package { get; set; }
        public bool? IsDiscontinued { get; set; }

        public Supplier Supplier { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }

        [Required]
        public string CreatedByRole { get; set; }
        [Required]
        public string SourcePerson { get; set; }

        public bool ApprovedByGeneralManager { get; set; }

        public DateTime Deleted { get; set; }

        public DateTime Updated { get; set; }
    }
}
