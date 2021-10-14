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
    public class ReservationRepository : IReservationRepository
    {
        //private connection string variable
        private readonly string _connectionString;
        public ReservationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }

        //gets all
        public async Task<IEnumerable<Reservation>> GetAll()
        {
            //sql query for get all reservations
            string query = @"SELECT re.rID, re.leaseBegin, re.leaseLast, s.name as status, c.cID, c.firstName, c.lastName, c.dateOfBirth, c.email, c.phoneNo, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, st.name as status
                            FROM Reservations re
                            LEFT JOIN Statuses s ON re.statusID = s.sID
                            INNER JOIN Customers c ON re.customerID = c.cID
                            INNER JOIN Vehicles v ON re.vehicleID = v.vID
                            INNER JOIN Statuses st ON v.statusID = st.sID;";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query with mapping: the result contains 3 types, with mapping it returns the Reservation with the Vehicle and Customer type properties
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var reservations = await connection.QueryAsync<Reservation, Customer, Vehicle, Reservation>(query, (reservation, customer, vehicle) =>
                    {
                        reservation.Customer = customer;
                        reservation.Vehicle = vehicle;
                        return reservation;
                    },
                    splitOn: "cID, vID");
                    return reservations.ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        //gets one by id
        public async Task<Reservation> GetByID(int id)
        {
            //query to get the reservation object
            string query = @"SELECT re.rID, re.leaseBegin, re.leaseLast, s.name as status, c.cID, c.firstName, c.lastName, c.dateOfBirth, c.email, c.phoneNo, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, st.name as status
                            FROM Reservations re
                            LEFT JOIN Statuses s ON re.statusID = s.sID
                            INNER JOIN Customers c ON re.customerID = c.cID
                            INNER JOIN Vehicles v ON re.vehicleID = v.vID
                            INNER JOIN Statuses st ON v.statusID = st.sID
                            WHERE re.rID = @id;";
            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query with mapping: the result contains 3 types, with mapping it returns the Reservation with the Vehicle and Customer type properties
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var reservations =  await connection.QueryAsync<Reservation, Customer, Vehicle, Reservation>(query, (reservation, customer, vehicle) =>
                    {
                        reservation.Customer = customer;
                        reservation.Vehicle = vehicle;
                        return reservation;
                    },
                        param: new { id = id},
                        splitOn: "cID, vID");
                    return reservations.FirstOrDefault();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be delete", exp);
            }
        }

        //creates one
        public async Task<Reservation> Insert(Reservation reservation)
        {
            //query for create a new reservation
            var query = @"INSERT INTO Reservations (leaseBegin, leaseLast, statusID, customerID, vehicleID) values(@lBegin, @lLast, (SELECT sID FROM Statuses WHERE name = @status), @customerID, @vehicleID); 
                          SELECT SCOPE_IDENTITY()";

            try
            {
                //connect
                using (var con = new SqlConnection(_connectionString))
                { 
                    //runs the query what returns the new reservations ID
                    //and the function returns the reservation with the new ID
                    reservation.RID =  await con.ExecuteScalarAsync<int>(query, reservation);
                    return reservation;
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be create", exp);
            }
        }

        //updates one
        public async Task Update(Reservation reservation)
        {
            //query for update dates, status, vehicle id (customer can not change)
            string query = @"UPDATE Reservations
                            SET statusID = (SELECT sID FROM Statuses WHERE name = @status),
	                            vehicleID = @vID,
                                leaseBegin = @lBegin,
                                leaseLast = @lLast
                            WHERE rID = @rID;";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query with a new object what contains the needed variables
                    var a = new
                    {
                        status = reservation.Status,
                        vID = reservation.Vehicle.VID,
                        lBegin = reservation.LeaseBegin,
                        lLast = reservation.LeaseLast,
                        rID = reservation.RID
                    };
                    await connection.ExecuteScalarAsync(query, a);
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
            //query for delete reservation by ID
            string query = @"DELETE FROM Reservations WHERE rID = @id";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    await connection.ExecuteScalarAsync(query, new { id = id });
                }
            }
            catch (Exception exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be delete", exp);
            }
        }

        public async Task UpdateStatus(int id, string status)
        {
            //query for update dates, status, vehicle id (customer can not change)
            string query = @"UPDATE Reservations
                            SET statusID = (SELECT s.sID FROM Statuses as s WHERE s.name = @status)
                            WHERE rID = @id;";
            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query with the id and statusName
                    await connection.ExecuteScalarAsync(query, new { status = status, id = id});
                }
            }
            catch (Exception exp)
            {

                throw new Exception("Data could not be update", exp);
            }

        }
    }
}
