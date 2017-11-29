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
    public class ProductController : Controller
    {
        public readonly ProductManager _productManager;

        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        [Route("products")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        public IActionResult GetAllProducts()
        {
            var response = _productManager.GetAllProducts().ToList();
            return Ok(response);
        }


        [Route("products/orderitem")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), 200)]
        public IActionResult GetAllProductAndOrdetItem()
        {
            var response = _productManager.GetCustomerAndOrdersAndOrderItems().ToList();
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
