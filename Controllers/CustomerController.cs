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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository userRepository)
        {
            _customerRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>>PostUser([FromBody] Customer user)
        {
            var newUser = await _customerRepository.Create(user);
            return newUser;
        }
    }
}
