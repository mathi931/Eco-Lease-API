using EcoLease_API.Models;
using EcoLease_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository userRepository)
        {
            _customerRepository = userRepository;
        }

        // GET api/Customers
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.GetAll();
        }

        // GET api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            return await _customerRepository.GetByID(id);
        }

        //POST api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>>InsertCustomer([FromBody] Customer customer)
        {
            var newCustomer = await _customerRepository.Insert(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.CID }, newCustomer);
        }

        // PUT api/Customers
        [HttpPut]
        public async Task<ActionResult> UpdateCustomer
            (int id, [FromBody] Customer customer)
        {
            if (id != customer.CID)
            {
                return BadRequest();
            }

            await _customerRepository.Update(customer);

            return NoContent();
        }

        // DELETE api/Customers
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCustomer(int id)
        {
            var customerToDelete = await _customerRepository.GetByID(id);
            if (customerToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _customerRepository.Remove(customerToDelete.CID);
                return NoContent();
            }
        }
    }
}
