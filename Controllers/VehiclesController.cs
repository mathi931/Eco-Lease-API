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

        // GET api/Vehicles
        [HttpGet]
        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await _vehicleRepository.GetAll();
        }

        // GET api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            return await _vehicleRepository.GetByID(id);
        }

        // POST api/Vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>>InsertVehicle([FromBody] Vehicle vehicle)
        {
            var newVehicle = await _vehicleRepository.Insert(vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { id = newVehicle.VId }, newVehicle);
        }

        // PUT api/Vehicles
        [HttpPut]
        public async Task<ActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.VId)
            {
                return BadRequest();
            }
     
            await _vehicleRepository.Update(vehicle);

            return NoContent();
        }

        // PATCH api/Vehicles
        [HttpPatch]
        public async Task<ActionResult> UpdateVehicleStatus(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.VId)
            {
                return BadRequest();
            }

            await _vehicleRepository.UpdateStatus(vehicle);

            return NoContent();
        }

        // DELETE api/Vehicles
        [HttpDelete]
        public async Task<ActionResult> Delete (int id)
        {
            var vehicleToDelete = await _vehicleRepository.GetByID(id);
            if(vehicleToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _vehicleRepository.Remove(vehicleToDelete.VId);
                return NoContent();
            }
        }
    }
}
