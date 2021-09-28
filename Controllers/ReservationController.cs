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

        [HttpPost]
        public async Task<ActionResult<Reservation>>PostRequest([FromBody] Reservation request)
        {
            var newRequest = await _reservationRepository.Create(request);
            return newRequest;
        }
    }
}
