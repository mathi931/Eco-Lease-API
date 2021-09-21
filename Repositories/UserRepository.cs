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
            var query = @"INSERT INTO Users(firstName, lastName, dateOfBirth) VALUES(@firstName, @lastName, @dateOfBirth); 
                          SELECT SCOPE_IDENTITY;";

            using (var connection = new SqlConnection(_connectionString))
            {
                user.UID = await connection.ExecuteScalarAsync<int>(query, user);
                return user;
            }
        }
    }
}
