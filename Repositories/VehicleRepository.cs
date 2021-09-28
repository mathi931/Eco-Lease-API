using Dapper;
using EcoLease_API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        //
        private readonly string _connectionString;
        public VehicleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }

        //get all
        public async Task<IEnumerable<Vehicle>> Get()
        {
            //sql query for get all
            string query = @"SELECT v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, s.name AS status
                             FROM Vehicles v 
                             LEFT JOIN Statuses s
                             ON v.statusID = s.sID";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (var connection = new SqlConnection(_connectionString))
                {
                    //returns the List of vehicles
                    return await connection.QueryAsync<Vehicle>(query);

                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        //get one by id
        public async Task<Vehicle> Get(int id)
        {
            //sql query for get a vehicle by ID
            string query = @"SELECT v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, s.name AS status
                             FROM Vehicles v 
                             LEFT JOIN Statuses s
                             ON v.statusID = s.sID
                             WHERE v.vID = @id";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    return await connection.QueryFirstOrDefaultAsync<Vehicle>(query, new { id = id });
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be delete", exp);
            }
        }
    }
}
