using System;
using System.Collections.Generic;

namespace CustomerManagerAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal? TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
