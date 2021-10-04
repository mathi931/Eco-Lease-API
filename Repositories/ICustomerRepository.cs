using EcoLease_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByID(int id);
        Task<Customer> Insert(Customer user);
        Task Update(Customer customer);
        Task Remove(int id);
    }
}
