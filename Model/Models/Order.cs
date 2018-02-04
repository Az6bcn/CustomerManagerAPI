using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagerAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
            ApprovedByGeneralManager = false;
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal? TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
        [Required]
        public string CreatedByRole { get; set; }
        [Required]
        public string SourcePerson { get; set; }

        public bool? ApprovedByGeneralManager { get; set; }

        public DateTime? Deleted { get; set; }

        public DateTime? Updated { get; set; }
    }
}
