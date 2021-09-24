using EcoLease_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public interface IReservationRepository
    {
        public Task<Reservation> Create(Reservation request);
    }
}
