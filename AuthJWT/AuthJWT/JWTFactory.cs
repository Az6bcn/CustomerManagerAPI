﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//JwtSecurityToken Class
// https://msdn.microsoft.com/en-us/library/system.identitymodel.tokens.jwtsecuritytoken(v=vs.114).aspx 

namespace AuthJWT.AuthJWT
{
    public class JWTFactory: IJWFactory
    {
        private readonly JWTIssuerOptions _jwtOptions;

        public JWTFactory(IOptions<JWTIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }




    public async Task<string> GenerateEncodedToken(string userName, IEnumerable<Claim> userIdentityClaims)
    {
            List<Claim> claims = new List<Claim>();
    
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            // loop through the usersClaims
            foreach (var item in userIdentityClaims)
            {
                claims.Add(new Claim(item.Type, item.Value));
            }


            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: _jwtOptions.NotBefore,
            expires: _jwtOptions.Expiration,
            signingCredentials: _jwtOptions.SigningCredentials);

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }


        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);





        private static void ThrowIfInvalidOptions(JWTIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JWTIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JWTIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JWTIssuerOptions.JtiGenerator));
            }
        }

        public Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            throw new NotImplementedException();
        }
    }
}
