using Microsoft.AspNetCore.Http;
using Model.Enumerations;
using Repositories.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace BusinessManager
{
    // Base class for Person --> Logged in person. 
    // we can easily use it to get the PersonID, PersonRole from the IHttpContextService to use anywhere in the API etc 
    public class BasePerson
    {
        
        static ManagerRolesEnum PersonRole { get; set; }

        private IHttpContextService _httpContextService;

        /* Dependency --> Inject the IHttpContextService*/
        public BasePerson(IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
            
        }
        
        public Guid CurrentPersonID() {
           return _httpContextService.getPersonID();
        }

        public ManagerRolesEnum CurrentPersonRole()
        {
            var roleFromHttpContext = _httpContextService.getRole();
            // Convert the string to enum value
            ManagerRolesEnum role = (ManagerRolesEnum)Enum.Parse(typeof(ManagerRolesEnum), roleFromHttpContext);
            return role;
        }
        

    }
}
