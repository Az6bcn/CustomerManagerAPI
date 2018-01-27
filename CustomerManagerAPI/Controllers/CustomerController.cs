using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessManager;
using CustomerManagerAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace CustomerManagerAPI.Controllers
{
    [Authorize(Policy = "GeneralManager")]
    [Route("api/[controller]")]  // ----> default root: http://localhost:port/api/customer
    public class CustomerController : Controller
    {
        private readonly CustomerManager _customerManager;

        public CustomerController(CustomerManager customerManager)
        {
            _customerManager = customerManager;
        }


        /// <summary>
        /// Gets the list of all customers
        /// </summary>
        /// <returns></returns>
        [Route("allcustomers")] //--> GET http://localhost:port/api/customer/allcustomers
        [HttpGet]
        [ProducesResponseType(typeof(Customer), 201)] //Specifies the type of response and the status code. --> So that Swagger can Show and Document the 2 status codes.
        [ProducesResponseType(typeof(Customer), 400)]
        public IActionResult GetAllCustomer()
        {
            var allCustomers = _customerManager.GetAllCustomer().ToList();
            return Ok(allCustomers);
        }



        [Route("allcustomersorder")] // GET http://localhost:port/api/customer/allcustomersorder
        [HttpGet]
        [ProducesResponseType(typeof(Customer), 201)] 
        [ProducesResponseType(typeof(Customer), 400)]
        public IActionResult GetCustomerAndOrder()
        {
            var allCustomerOrder = _customerManager.GetCustomersAndOrder().ToList();
            return Ok(allCustomerOrder);
        }



        /// <summary>
        /// Gets a specific customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")] //--> GET http://localhost:port/api/customer/id
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Gets the details of a specific customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}/details")] //--> GET api/customer/id/details 
        [HttpGet]
        public string GetDetails(int id)
        {
            return "value";
        }


        // POST api/customer  POST: http://localhost:port/api/customer
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}


/* https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api
 * return new ObjectResult(item) --------> returns 200 with a JSON response body with just a single item.
 * return new NoContentResult(); --> when the action is done and the response does not include an entity*/
