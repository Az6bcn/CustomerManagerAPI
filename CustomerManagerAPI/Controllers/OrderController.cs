using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessManager;
using CustomerManagerAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerManagerAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {

        private readonly OrderManager _orderManager;

        public OrderController(OrderManager orderManager)
        {
            _orderManager = orderManager;
        }


        [Route("orders")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), 200)]
        public IActionResult GetAllOrders()
        {
            var response = _orderManager.GetAllOrders().ToList();
            return Ok(response);
        }


        [Route("orders/orderitem")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), 200)]
        public IActionResult GetAllOrdersAndOrderItemOfCustomers()
        {
            var response = _orderManager.GetCustomerAndOrdersAndOrderItems().ToList();
            return Ok(response);
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
