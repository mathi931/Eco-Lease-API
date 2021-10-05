﻿using Dapper;
using EcoLease_API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Repositories
{
    public class AgreementRepository : IAgreementRepository
    {
        //private connection string variable
        private readonly string _connectionString;
        public AgreementRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }

        //gets a filename by ID
        public async Task<Agreement> GetByID(int id)

        {   //get fileName
            var query = @"SELECT a.fileName from Agreements as a WHERE a.reservationID = @id";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    return await connection.QueryFirstOrDefaultAsync<Agreement>(query, new { id = id });
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be delete", exp);
            }
        }

        //creates one
        public async Task<Agreement> Insert(Agreement agreement)
        {
            //query to inserts a new agreement
            var query = @"INSERT INTO Agreements(fileName, reservationID) VALUES(@doc, @rID)
                          SELECT IDENT_CURRENT('Agreements')";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query and returns the new objects ID
                    agreement.AID = await connection.ExecuteScalarAsync<int>(query, new
                    {
                        doc = agreement.FileName,
                        rID = agreement.Reservation.RId
                    });

                    //returns the new object
                    return agreement;
                }
            }
            catch (Exception exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be insert", exp);
            }
        }

        //removes one by ID
        public async Task Remove(int id)
        {
            //query for delete agreement by ID
            string query = @"DELETE FROM Agreements WHERE sID = @id";

            try
            {
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
    }
}
