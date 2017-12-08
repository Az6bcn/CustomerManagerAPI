using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please provide your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please give a password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Firstname can not be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please give the Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Claims Can't be empty")]
        public ManagerRolesEnum Role { get; set; }
        //public IEnumerable<String> Claims { get; set; }
    }
}
