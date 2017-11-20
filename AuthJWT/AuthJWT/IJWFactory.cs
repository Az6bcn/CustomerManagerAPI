using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthJWT.AuthJWT
{
    public interface IJWFactory
    {

        Task<string> GenerateEncodedToken(string userName, IEnumerable<Claim> userIdentityClaims);
        // ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
