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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository requestRepository)
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
            return CreatedAtAction(nameof(GetReservation), new { id = newRequest.RId }, newRequest);
        }

        // PUT api/Reservations
        [HttpPut]
        public async Task<ActionResult> UpdateReservations(int id, [FromBody] Reservation reservation)
        {
            if (id != reservation.RId)
            {
                return BadRequest();
            }

            await _reservationRepository.Update(reservation);

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
                await _reservationRepository.Remove(reservationToDelete.RId);
                return NoContent();
            }
        }
    }
}
