using EcoLease_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
    }
}
