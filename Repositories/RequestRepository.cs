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
    public class RequestRepository : IRequestRepository
    {
        private readonly string _connectionString;

        public RequestRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }


        public async Task<Request> Create(Request request)
        {
            var query = @"INSERT INTO Requests(leaseBegin, leaseLast, userID, vehicleID) VALUES(@leaseBegin, @leaseLast, @userID, @vehicleID); SELECT SCOPE_IDENTITY()";

            using(var con = new SqlConnection(_connectionString))
            {
                try
                {
                    request.RID =  await con.ExecuteScalarAsync<int>(query, request);
                    return request;
                }
                catch (SqlException exp)
                {
                    //throws an error if the data access is unsucsessfull
                    throw new InvalidOperationException("Data could not be create", exp);
                }

            }
        }
    }
}
