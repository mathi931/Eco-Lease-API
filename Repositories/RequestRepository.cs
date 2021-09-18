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
            var getRequestID = @"SELECT IDENT_CURRENT('Requests')";

            var query = @"INSERT INTO Requests(userID, vehicleID, statusID) VALUES(@userID, @vehicleID, @statusID)";

            using(var con = new SqlConnection(_connectionString))
            {
                try
                {

                    await con.ExecuteAsync(query, request);

                    var rID = await con.ExecuteAsync(getRequestID);

                    return new Request{
                        RID = rID,
                        UserID = request.UserID,
                        VehicleID = request.VehicleID,
                        StatusID = request.StatusID
                    };

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
