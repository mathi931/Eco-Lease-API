using EcoLease_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public interface IAgreementRepository
    {
        Task<Agreement> GetByID(int id);
        Task<Agreement> Insert(Agreement agreement);
        Task Remove(int id);
    }
}
