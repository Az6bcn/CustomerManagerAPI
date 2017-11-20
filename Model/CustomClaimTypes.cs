using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CustomClaimTypes
    {
        public string Firstname { get; set; } = "Firstname";
        public string Lastname { get; set; } = "Lastname";
        public string Username { get; set; } = "Username";
        public ManagerRolesEnum ManagerRole { get; set; }
        public string CanCreateCustomer { get; set; } = "CanCreateCustomer";
        public string CanCreateProduct { get; set; } = "CanCreateProduct";

    }
}
