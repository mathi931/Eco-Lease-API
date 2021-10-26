using EcoLease_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAll();
        Task<Reservation> GetByID(int id);
        Task<Reservation> Insert(Reservation reservation);
        Task Update(Reservation reservation);
        Task UpdateStatus(int id, string status);
        Task Remove(int id);

    }
}
