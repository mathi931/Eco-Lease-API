using EcoLease_API.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System;

namespace EcoLease_API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }


        public async Task<Customer> Create(Customer user)
        {
            var query = @"INSERT INTO Customers(firstName, lastName, email, phoneNo, dateOfBirth) VALUES(@firstName, @lastName, @email, @phoneNo, @dateOfBirth); 
                          SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    user.CID = await connection.ExecuteScalarAsync<int>(query, user);
                    return user;
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
