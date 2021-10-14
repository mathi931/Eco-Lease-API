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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

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
            return await _reservationRepository.GetByID(id);
        }

        // POST api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>>PostRequest([FromBody] Reservation request)
        {
            var newRequest = await _reservationRepository.Insert(request);
            return CreatedAtAction(nameof(GetReservation), new { id = newRequest.RID }, newRequest);
        }

        // PUT api/Reservations
        [HttpPut]
        public async Task<ActionResult> UpdateReservation(int id, [FromBody] Reservation reservation)
        {
            if (id != reservation.RID)
            {
                return BadRequest();
            }

            await _reservationRepository.Update(reservation);

            return NoContent();
        }

        //PUT api/Reservations/status
        [HttpPut("status")]
        public async Task<ActionResult> UpdateReservationStatus(int id, [FromBody] string status)
        {
            var reservationToUpdate = await _reservationRepository.GetByID(id);
            if (reservationToUpdate == null)
            {
                return BadRequest();
            }
            await _reservationRepository.UpdateStatus(id, status);

            return NoContent();
        }

        // DELETE api/Reservations
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var reservationToDelete = await _reservationRepository.GetByID(id);
            if (reservationToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await _reservationRepository.Remove(reservationToDelete.RID);
                return NoContent();
            }
        }
    }
}
