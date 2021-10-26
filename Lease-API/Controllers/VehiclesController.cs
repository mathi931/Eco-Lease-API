using EcoLease_API.Models;
using EcoLease_API.Repositories;
using EcoLease_API.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using EcoLease_API.Services;

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

        VehicleValidator validatorVehicle = new VehicleValidator();
        IDValidator validatorID = new IDValidator();

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
            ValidationResult validation = validatorID.Validate(id);

            if (!validation.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

            return await _vehicleRepository.GetByID(id);
        }

        // POST api/Vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>>InsertVehicle([FromBody] Vehicle vehicle)
        {
            ValidationResult validation = validatorVehicle.Validate(vehicle);

            if (!validation.IsValid)
            {
                return BadRequest("Wrong Parameters!");
            }

            var newVehicle = await _vehicleRepository.Insert(vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { id = newVehicle.VID }, newVehicle);
        }

        // PUT api/Vehicles/
        [HttpPut]
        public async Task<ActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            ValidationResult validationVehicle = validatorVehicle.Validate(vehicle);
            ValidationResult validationID = validatorID.Validate(id);

            if (id != vehicle.VID || !validationID.IsValid || !validationVehicle.IsValid)
            {
                return BadRequest();
            }

            await _vehicleRepository.Update(vehicle);

            return NoContent();
        }

        // PUT api/Vehicles/status
        [HttpPut("status")]
        public async Task<ActionResult> UpdateVehicleStatus(int id, [FromBody] Vehicle vehicle)
        {
            ValidationResult validationID = validatorID.Validate(id);

            if (id != vehicle.VID || !validationID.IsValid)
            {
                return BadRequest();
            }

            await _vehicleRepository.UpdateStatus(vehicle);

            return NoContent();
        }

        // DELETE api/Vehicles
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveVehicle (int id)
        {
            ValidationResult validationID = validatorID.Validate(id);
            if (!validationID.IsValid)
            {
                return BadRequest();
            }

            var vehicleToDelete = await _vehicleRepository.GetByID(id);
            if(vehicleToDelete == null || !validationID.IsValid)
            {
                return NotFound();
            }
            else
            {
                await _vehicleRepository.Remove(vehicleToDelete.VID);
                return NoContent();
            }
        }
    }
}
