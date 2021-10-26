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
        //private connection string variable
        private readonly string _connectionString;
        public VehicleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }

        //gets all
        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            //sql query for get all
            string query = @"SELECT v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, s.name AS status
                             FROM Vehicles v 
                             LEFT JOIN Statuses s
                             ON v.statusID = s.sID";

            try
            {
                //connects
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

        //gets one by id
        public async Task<Vehicle> GetByID(int id)
        {
            //sql query for get a vehicle by ID
            string query = @"SELECT v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, s.name AS status
                             FROM Vehicles v 
                             LEFT JOIN Statuses s
                             ON v.statusID = s.sID
                             WHERE v.vID = @id";
            try
            {
                //connects
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

        //creates one
        public async Task<Vehicle> Insert(Vehicle vehicle)
        {
            //sql query for insert the new vehicle
            string query = @"INSERT INTO Vehicles (make, model, registered, plateNo, km, notes, img, price, statusID) values(@make, @model, @registered, @plateNo, @km, @notes, @img, @price, (SELECT sID FROM Statuses WHERE name = @status))
                  SELECT IDENT_CURRENT('Vehicles') 
                ";
            //
            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    vehicle.VID = await connection.ExecuteScalarAsync<int>(query, vehicle);
                    //var v =  new Vehicle(id, vehicle.Make,vehicle.Model,vehicle.Registered, vehicle.PlateNo, vehicle.Km, vehicle.Notes, vehicle.Img,vehicle.Price, vehicle.Status);

                    return vehicle;
                }
            }
            catch(Exception exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be insert", exp);
            }
        }

        //updates one
        public async Task Update(Vehicle vehicle)
        {
            //updates the status of reserved vehicle
            string query = @"UPDATE Vehicles SET make = @make, model = @model, registered = @registered, plateNo = @plateNo, km = @km, notes = @notes, img = @img, price = @price, statusID = (SELECT sID FROM Statuses WHERE name = @status) WHERE vID = @VId"
               ;

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    await connection.ExecuteAsync(query, vehicle);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be update", exp);
            }
        }

        //updates the status property
        public async Task UpdateStatus(Vehicle vehicle)
        {
            //updates the status of reserved vehicle
            string query = @"UPDATE Vehicles SET statusID = (SELECT sID FROM Statuses WHERE name = @status) WHERE vID = @VId";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    await connection.ExecuteAsync(query, vehicle);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be update", exp);
            }
        }

        //removes one
        public async Task Remove(int id)
        {
            //query for delete vehicle by ID
            string query = @"DELETE FROM Vehicles WHERE vID = @id";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    await connection.ExecuteScalarAsync(query, new { id = id});
                }
            }
            catch (Exception exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be delete", exp);
            }
        }
    }
}
