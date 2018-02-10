using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Services
{
    public interface IHttpContextService
    {
        Guid getPersonID();
        string getRole();
    }
}
