using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagerAPI.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Product = new HashSet<Product>();
            ApprovedByGeneralManager = false;
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public ICollection<Product> Product { get; set; }
        [Required]
        public string CreatedByRole { get; set; }
        [Required]
        public string SourcePerson { get; set; }

        public bool ApprovedByGeneralManager { get; set; }

        public DateTime Deleted { get; set; }

        public DateTime Updated { get; set; }
    }
}
