using EcoLease_API.Models;
using EcoLease_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EcoLease_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;

        public StatusesController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }


        // GET: api/Statuses
        [HttpGet]
        public async Task<IEnumerable<Status>> GetStatuses()
        {
            return await _statusRepository.GetAll();
        }

    }
}
