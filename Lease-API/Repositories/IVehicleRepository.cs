using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoLease_API.Models;

namespace EcoLease_API.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetByID(int id);
        Task<Vehicle> Insert(Vehicle vehicle);
        Task Update(Vehicle vehicle);
        Task UpdateStatus(Vehicle vehicle);
        Task Remove(int id);

    }
}
