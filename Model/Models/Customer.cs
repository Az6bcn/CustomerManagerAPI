using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagerAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
            ApprovedByGeneralManager = false;

        }
        
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
        
        public DateTime Created { get; set; }
        // [Required]
        public string CreatedByRole { get; set; }
        // [Required]
        public string SourcePerson { get; set; }
        
        public bool? ApprovedByGeneralManager { get; set; }
        
        public DateTime? Deleted { get; set; }
        
        public DateTime? Updated { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
