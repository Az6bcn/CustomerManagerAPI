using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager
{
    // Base class for Person --> Logged in person. 
    // we can easily use it to get the PersonID, PersonRole to use anywhere in the API etc 
    public class BasePerson
    {
        static string PersonID { get; set; }
        static ManagerRolesEnum PersonRole { get; set; }
    }
}
