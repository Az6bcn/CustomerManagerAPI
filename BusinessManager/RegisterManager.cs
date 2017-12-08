using CustomerManagerAPI.DictStrategy;
using Microsoft.AspNetCore.Identity;
using Model;
using Model.Enumerations;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessManager
{

    public class RegisterManager
    {
        // ASPIdentity context
        private readonly AspNetUserContext _identityContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private bool result;
        private bool RoleExists;
        AppUser user;

        public RegisterManager(AspNetUserContext identityContext, UserManager<AppUser> usermanager
            , RoleManager<IdentityRole> roleManager)
        {

            _identityContext = identityContext;
            _userManager = usermanager;
            _roleManager = roleManager;

        }

        // https://stackoverflow.com/questions/20383955/how-to-add-claims-in-asp-net-identity
        /// <summary>
        /// Handles Registration of Application User 
        /// </summary>
        public async Task<AppUser> RegistrationAddRoleAndClaims(RegistrationViewModel model)
        {
            // create the appUser object for the new user BD.
            AppUser appUser = new AppUser
            {
                FirstName = model.FirstName,
                Email = model.Email,
                UserName = model.Email,
                LastName = model.LastName
            };

            

            IdentityResult result;
            try
            {
                //call manager to create user in '[AspNetUsers] Table'
                result = await _userManager.CreateAsync(appUser, model.Password);
            }
            catch (Exception e)
            {

                throw new Exception("Duplicated User");
            }


            if (result.Succeeded)
            {
                // check if incoming role exist, if it doesn't creates it
                var result2 = await CheckRoleExistOrCreateRole(model);
                
                // Add the users Role
                await _userManager.AddToRoleAsync(appUser, model.Role.ToString());

                // add the Users Claims
                await AddClaimsToUser(model);

                //get the created user and return it.
                user = await _userManager.FindByEmailAsync(model.Email);
            }

            if(!result.Succeeded) { throw new Exception("Duplicated User"); }

            return user;
        }


        private async Task<bool> CheckRoleExistOrCreateRole(RegistrationViewModel model)
        {
            
            string roleToBeCreated = model.Role.ToString();

            RoleExists = await _roleManager.RoleExistsAsync(model.Role.ToString());
            // check if role does not exist
            if (!RoleExists)
            {
                var response = await _roleManager.CreateAsync(new IdentityRole(roleToBeCreated));

                result = (response.Succeeded == true) ? true : false;

                return result;
            }

            return true;
        }



        private async Task<bool> AddClaimsToUser(RegistrationViewModel userRegistered)
        {
            
            // get the claims for the user roles 
            ClaimsForRolesStrategy claimsFromStrategy = new ClaimsForRolesStrategy();
            var claimsForUserRole = claimsFromStrategy.getManagerClaims(userRegistered.Role);
            
            //add the Claims for the User
            CustomClaimTypes myClaims = new CustomClaimTypes();

            try
            {
                // retrieve the user to add claims to 
                var user = await _userManager.FindByEmailAsync(userRegistered.Email);

                //List of Claims
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim("Firstname", user.FirstName));
                userClaims.Add(new Claim(myClaims.Lastname, user.LastName));
                userClaims.Add(new Claim(myClaims.CanCreateCustomer, myClaims.CanCreateCustomer = claimsForUserRole.CanCreateCustomer.ToString()));
                userClaims.Add(new Claim(myClaims.CanCreateProduct, myClaims.CanCreateProduct = claimsForUserRole.CanCreateProduct.ToString()));
                userClaims.Add(new Claim("Role", userRegistered.Role.ToString()));

                // add ListOfClaims for the user
                var isUserClaimsAddedSuccesfully = await _userManager.AddClaimsAsync(user, userClaims);

                return isUserClaimsAddedSuccesfully.Succeeded == true ? true : false;
            }
            catch (Exception e)
            {

                throw new Exception("The Claims could not be added", e);
            }

            
        }

        

        // http://benfoster.io/blog/asp-net-identity-role-claims --> Creates Role and Add claims to those roles(RoleClaims).



    }
}
