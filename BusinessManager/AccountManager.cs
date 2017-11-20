using AuthJWT.AuthJWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Model;
using Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager
{
    public class AccountManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JWTIssuerOptions _jwtOptions;  // To access properties to Create and  Modify Token
        private readonly IJWFactory _jwtFactory; // Factory to generate JWTokens and Claims
        private readonly JsonSerializerSettings _serializerSettings; // To serialize data in JSON format. Newtonsoft

        public AccountManager(UserManager<AppUser> usermanager, IJWFactory jwtFactory, IOptions<JWTIssuerOptions> jwtOptions)
        {
            _userManager = usermanager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }


        public async Task<bool> LoginUser(CredentialsViewModel userCredentials)
        {
            // find user
            var userToLogin = await _userManager.FindByEmailAsync(userCredentials.Username);

            if (userToLogin != null)
            {
                // verify the associated password
             return (await _userManager.CheckPasswordAsync(userToLogin, userCredentials.Password)) ? true : false;
            }
            return false;
        }



        //// Checks and verifies user exist, password is valid for the user and then generates the claims.
        //private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        //{
        //    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
        //    {
        //        // get the username to verify if it exist in the database, using the 'UserManager'.
        //        var userToVerify = await _userManager.FindByNameAsync(userName);

        //        if (userToVerify != null)
        //        {
        //            // check if the password is valid for the User verified  
        //            if (await _userManager.CheckPasswordAsync(userToVerify, password))
        //            {
        //                // call JwtFactory to generate claims for the user.
        //                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
        //            }
        //        }
        //    }

        //    // Credentials are invalid, or account doesn't exist
        //    return await Task.FromResult<ClaimsIdentity>(null);
        //}




        private async Task<IEnumerable<Claim>> GetUserClaims(CredentialsViewModel appUser)
        {
            //find the user
            var user = await _userManager.FindByNameAsync(appUser.Username);

            // return claims of thE USER
            var userClaims = await _userManager.GetClaimsAsync(user);

            return userClaims;
        }


        // Checks and verifies user exist, password is valid for the user and then generates the claims.
        public async Task<dynamic> GetFactoryTogenerateTokenWithClaims(CredentialsViewModel credentials)
        {
          var userIdentityClaims = await GetUserClaims(credentials);

            // Serialize and return the response as JSON.
            var response = new
            {
                // call JwtFactory to generate encoded Token.
                auth_token = await Task.FromResult(_jwtFactory.GenerateEncodedToken(credentials.Username, userIdentityClaims)),
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            return response;
        }

     
    }
}
