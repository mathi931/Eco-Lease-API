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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await _vehicleRepository.Get();
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            return await _vehicleRepository.Get(id);
        }
    }
}
