using Dapper;
using EcoLease_API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        //private connection string variable
        private readonly string _connectionString;
        public StatusRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }

        public async Task<IEnumerable<Status>> GetAll()
        {
            //sql query for get all statuses
            string query = @"SELECT * FROM Statuses";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    return await connection.QueryAsync<Status>(query);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }
    }
}
