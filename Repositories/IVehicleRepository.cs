using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoLease_API.Models;

namespace EcoLease_API.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> Get();
        Task<Vehicle> Get(int id);
    }
}
