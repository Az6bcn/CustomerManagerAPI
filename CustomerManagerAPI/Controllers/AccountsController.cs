using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels;
using CustomerManagerAPI.ActionFilter;
using Model;
using Microsoft.AspNetCore.Identity;
using BusinessManager;
using AuthJWT.AuthJWT;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Model.Models;
using CustomerManagerAPI.ExceptionFilter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagerAPI.Controllers
{
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        
        private readonly JsonSerializerSettings _serializerSettings; // To serialize data in JSON format. Newtonsoft
       
        public readonly RegisterManager _manager;
        private readonly AccountManager _accountManager;
        private dynamic jsonToken;

        public AccountsController(RegisterManager manager, AccountManager accountManager)
        {
            _manager = manager;
            _accountManager = accountManager;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }



        // POST api/accounts
        [HttpPost]
        [ValidateModel] // Validates the Model with ActionFilter before executing the logic in this Action()
        [ProducesResponseType(typeof(RegistrationViewModel), 201)]
        [ProducesResponseType(typeof(RegistrationViewModel), 400)]
        public async Task<IActionResult> PostRegister([FromBody]RegistrationViewModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult("Error");
            }

            var response = await _manager.RegistrationAddRoleAndClaims(model);

            return Ok(response);
        }



        // POST api/auth/login
        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> PostLogin([FromBody]CredentialsViewModel credentials)
        {

            var isUserLoggedIn = await _accountManager.LoginUser(credentials);

            if (isUserLoggedIn) {
             var identity = await _accountManager.GetFactoryTogenerateTokenWithClaims(credentials);

                jsonToken = JsonConvert.SerializeObject(identity, _serializerSettings);
            }
            else
            {
                return new BadRequestObjectResult(new Error { ErrorMessage = "Username or Password incorrect" });
           
            }
            
            return new OkObjectResult(jsonToken);
        }


       

    }

    
     
}


/* 
     Routes Attributes
      https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
     */
