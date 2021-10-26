using System.Collections.Generic;
using System.Threading.Tasks;
using EcoLease_API.Models;
using EcoLease_API.Repositories;
using EcoLease_API.Validators;
using Microsoft.AspNetCore.Mvc;

namespace EcoLease_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IDValidator validatorID = new();


        //instance of validator
        private readonly ReservationValidator validatorReservation = new();

        public ReservationsController(IReservationRepository requestRepository)
        {
            _reservationRepository = requestRepository;
        }

        // GET api/Reservations
        [HttpGet]
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _reservationRepository.GetAll();
        }

        // GET api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var validation = validatorID.Validate(id);
            if (!validation.IsValid) return BadRequest("Wrong Parameters!");

            return await _reservationRepository.GetByID(id);
        }

        // POST api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostRequest([FromBody] Reservation request)
        {
            var validation = validatorReservation.Validate(request);
            var validationCustomerID = validatorID.Validate(request.Customer.CID);
            var validationVehicleID = validatorID.Validate(request.Vehicle.VID);

            if (!validation.IsValid || !validationCustomerID.IsValid || !validationVehicleID.IsValid)
                return BadRequest("Wrong Parameters!");

            var newRequest = await _reservationRepository.Insert(request);
            return CreatedAtAction(nameof(GetReservation), new {id = newRequest.RID}, newRequest);
        }

        // PUT api/Reservations
        [HttpPut]
        public async Task<ActionResult> UpdateReservation(int id, [FromBody] Reservation reservation)
        {
            var validation = validatorReservation.Validate(reservation);
            var validationID = validatorID.Validate(id);

            if (id != reservation.RID || !validationID.IsValid) return BadRequest("Wrong Parameters!");

            if (!validation.IsValid) return BadRequest("Wrong Parameters!");

            await _reservationRepository.Update(reservation);

            return NoContent();
        }

        //PUT api/Reservations/status
        [HttpPut("status")]
        public async Task<ActionResult> UpdateReservationStatus(int id, [FromBody] string status)
        {
            var validationID = validatorID.Validate(id);
            if (!validationID.IsValid) return BadRequest("Wrong Parameters!");

            var reservationToUpdate = await _reservationRepository.GetByID(id);
            if (reservationToUpdate == null) return BadRequest();
            await _reservationRepository.UpdateStatus(id, status);

            return NoContent();
        }

        // DELETE api/Reservations
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var validationID = validatorID.Validate(id);
            if (!validationID.IsValid) return BadRequest("Wrong Parameters!");

            var reservationToDelete = await _reservationRepository.GetByID(id);
            if (reservationToDelete == null)
            {
                return NotFound();
            }

            await _reservationRepository.Remove(reservationToDelete.RID);
            return NoContent();
        }
    }
}