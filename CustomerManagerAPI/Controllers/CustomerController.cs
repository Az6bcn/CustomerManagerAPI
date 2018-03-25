using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessManager;
using CustomerManagerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using CustomerManagerAPI.ActionFilter;
using Model.Models;
using System.Diagnostics;

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
        /// Get all customers
        /// </summary>
        /// <returns></returns>
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
        /// Gets the list of all customers that are approved by General Manager
        /// </summary>
        /// <returns></returns>
        [Route("allcustomers")] //--> GET http://localhost:port/api/customer/allcustomers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 201)] //Specifies the type of response and the status code. --> So that Swagger can Show and Document the 2 status codes.
        [ProducesResponseType(typeof(IEnumerable<Customer>), 400)]
        public IActionResult GetAllCustomer()
        {
            var allCustomers = _customerManager.GetAllCustomersApprovedByGeneralManagerAsync().Result;
            return Ok(allCustomers);
        }

        /// <summary>
        /// Gets the list of all customers created by GeneralManager
        /// </summary>
        /// <returns></returns>
        [Route("allcustomers-createdby-general-manager")] //--> GET http://localhost:port/api/customer/allcustomers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 201)] //Specifies the type of response and the status code. --> So that Swagger can Show and Document the 2 status codes.
        [ProducesResponseType(typeof(IEnumerable<Customer>), 400)]
        public IActionResult GetAllCustomerCreatedByGeneralManagerAsync()
        {
            var allCustomers = _customerManager.GetAllCustomersApprovedByGeneralManagerAsync().Result;
            return Ok(allCustomers);
        }


        /// <summary>
        /// Gets the list of all customers created by CustomerManager
        /// </summary>
        /// <returns></returns>
        [Route("allcustomers-createdby-customer-manager")] //--> GET http://localhost:port/api/customer/allcustomers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 201)] //Specifies the type of response and the status code. --> So that Swagger can Show and Document the 2 status codes.
        [ProducesResponseType(typeof(IEnumerable<Customer>), 400)]
        public IActionResult GetAllCustomerCreatedByCustomerManager()
        {
            var allCustomers = _customerManager.GetAllCustomersApprovedByCustomerManagerAsync().Result;
            return Ok(allCustomers);
        }


        /// <summary>
        /// Gets the list of all customers created by ProductManager
        /// </summary>
        /// <returns></returns>
        [Route("allcustomers-createdby-product-manager")] //--> GET http://localhost:port/api/customer/allcustomers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 201)] //Specifies the type of response and the status code. --> So that Swagger can Show and Document the 2 status codes.
        [ProducesResponseType(typeof(IEnumerable<Product>), 400)]
        public IActionResult GetAllCustomerCreatedByProductManager()
        {
            var allCustomers = _customerManager.GetAllProductsApprovedByGeneralManagerAsync().Result;
            return Ok(allCustomers);
        }


        /// <summary>
        /// Gets the list of all customers created by SectionManager
        /// </summary>
        /// <returns></returns>
        [Route("allcustomers-createdby-section-manager")] //--> GET http://localhost:port/api/customer/allcustomers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 201)] //Specifies the type of response and the status code. --> So that Swagger can Show and Document the 2 status codes.
        [ProducesResponseType(typeof(IEnumerable<Customer>), 400)]
        public IActionResult GetAllCustomerCreatedBySectionManagerAsync()
        {
            var allCustomers = _customerManager.GetAllCustomerCreatedBySectionManagerAsync().Result;
            return Ok(allCustomers);
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
        [ValidateModel]
        [ProducesResponseType(typeof(Customer), 201)] //Specifies the type of response and the status code. --> So that Swagger can Show and Document the 2 status codes.
        [ProducesResponseType(typeof(Customer), 400)]
        public async Task<IActionResult> Post([FromBody]Customer customer)
        {
            if (customer == null) return new BadRequestObjectResult(customer);

            var addCustomer = await _customerManager.addCustomerStoredProcedure(customer);
            return new ObjectResult(addCustomer);
        }


        /// <summary>
        /// Edit a selected customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerToEdit"></param>
        /// <returns></returns>
        // PUT api/customer/5
        [Route("edit-customer"+"/{id}")] // PUT: http://localhost:port/api/customer/edit-customer/id
        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Put (int id, [FromBody] Customer customerToEdit)
        {
            if (customerToEdit == null) { return new BadRequestObjectResult(new Error { ErrorMessage = "Invalid Object to Update" }); };

            if (id != customerToEdit.Id) { return new BadRequestObjectResult(new Error { ErrorMessage = "Object Id's don't match" }); };

            var response = await _customerManager.updateCustomerStoredProcedureAsync(customerToEdit);

            return new OkObjectResult(response);
        }


        /// <summary>
        /// Deletes a selected customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        // DELETE api/customer/5
        [HttpDelete("delete-customer" + "/{customerID}")] // Delete: http://localhost:port/api/customer/edit-customer/customerID
        public async Task<IActionResult> Delete(int customerID)
        {
            if (customerID < 0 || customerID == 0)
            {
                return new BadRequestObjectResult(new Error() { ErrorMessage = "item id cannot be less than zero or empty" });
            }

            var response = await _customerManager.deleteCustomer(customerID);

            return (response != null) ? new NoContentResult() : (IActionResult)new BadRequestObjectResult(new Error() { ErrorMessage = "customerID" });
        }

        /// <summary>
        /// Mark the customer as approve
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [Route("approve-customer")] // PUT: http://localhost:port/api/customer/approve-customer/id
        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> ApproveCustomer( [FromBody]Customer customer)
        {
            if (customer == null)
            {
                return new BadRequestObjectResult(new Error() { ErrorMessage = "customer object null" });
            }

            var response = await _customerManager.ApproveCustomer(cus: customer);

            return new OkObjectResult(response);
        }


        /// <summary>
        /// Mark the product as approve
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [Route("approve-product")] // PUT: http://localhost:port/api/customer/approve-customer/id
        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> ApproveProduct([FromBody]Product product)
        {
            if (product == null)
            {
                return new BadRequestObjectResult(new Error() { ErrorMessage = "product object null" });
            }

            var response = await _customerManager.ApproveProduct(product: product);

            return new OkObjectResult(response);
        }
    }
}


/* https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api
 * return new ObjectResult(item) --------> returns 200 with a JSON response body with just a single item.
 * return new NoContentResult(); --> when the action is done and the response does not include an entity
 
     
     
    // await USED WRONGLY
public async Task<List > GetAlComplete()
{
query = await _connection.Table().Where(v => v._naam.StartsWith("A"));
return await query.ToListAsync();
}


CORRECTLY:

You need to remove the "await" from the first line in your method. There is no async operation in this line, so you cannot await it. The second line is Async (ToListAsync), so you need to await it. So, it's not a SQLite issue.

public async Task<List > GetAlComplete()
{
query = _connection.Table().Where(v => v._naam.StartsWith("A"));
return await query.ToListAsync();
} 
*/

