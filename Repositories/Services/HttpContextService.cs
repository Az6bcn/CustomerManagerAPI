using Microsoft.AspNetCore.Http;
using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Repositories.Services
{
    public class HttpContextService: IHttpContextService
    {
        private IHttpContextAccessor _httpContextAccessor;

        /* Dependency --> Inject the IHttpContextAccessor, to acces the HttpContext and 
        manipulate http request, response etc. */
        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid getPersonID()
        {
            var userLoggedIn = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            if (userLoggedIn && _httpContextAccessor.HttpContext.User != null)
            {

                return new Guid (_httpContextAccessor.HttpContext.User.FindFirstValue("aspNetID"));
            }
            return Guid.Empty;  // "00000000-0000-0000-0000-000000000000"
        }

        public string getRole()
        {
            var userLoggedIn = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            if (userLoggedIn && _httpContextAccessor.HttpContext.User != null)
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue("Role"); 
            }

            return null;
        }
    }
}
