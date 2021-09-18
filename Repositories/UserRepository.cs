using EcoLease_API.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace EcoLease_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }


        public async Task<User> Create(User user)
        {
            var query = @"INSERT INTO Users(firstName, lastName, dateOfBirth) VALUES(@firstName, @lastName, @dateOfBirth)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, user);
                return user;
            }
        }
    }
}
