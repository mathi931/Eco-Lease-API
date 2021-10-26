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
        //gets the private connection string variable to use localy
        private readonly string _connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EcoLeaseDB");
        }

        //gets all the customers
        public async Task<IEnumerable<Customer>> GetAll()
        {
            //query for select all the customers
            string query = @"SELECT * FROM Customers";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query and returns the list of objects
                    return await connection.QueryAsync<Customer>(query);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be reach", exp);
            }
        }

        //gets a customer by ID
        public async Task<Customer> GetByID(int id)
        {
            //query for selecting the customer by ID
            string query = @"SELECT * FROM Customers WHERE cID = @id";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { id = id });
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be reach", exp);
            }
        }

        //inserts a new customer
        public async Task<Customer> Insert(Customer customer)
        {
            //query for insert a new customer and returns the ID
            var query = @"INSERT INTO Customers(firstName, lastName, email, phoneNo, dateOfBirth) VALUES(@firstName, @lastName, @email, @phoneNo, @dateOfBirth); 
                         SELECT IDENT_CURRENT('Customers');";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query what returns the ID, set the ID of passed customer
                    customer.CID = await connection.ExecuteScalarAsync<int>(query, customer);

                    //returns the full object
                    return customer;
                }
            }
            catch (Exception exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be reach", exp);
            }
        }

        //removes a customer
        public async Task Remove(int id)
        {
            //query for delete customer by ID
            string query = @"DELETE FROM Customers WHERE cID = @id";

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
                throw new InvalidOperationException("Data could not be reach", exp);
            }
        }

        //updates a customer
        public async Task Update(Customer customer)
        {
            //updates the customer query
            var query = @"UPDATE Customers SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth, email = @email, phoneNo = @phoneNo WHERE cID = @cid";

            try
            {
                //connects
                using (var connection = new SqlConnection(_connectionString))
                {
                    //runs the query
                    await connection.ExecuteAsync(query, customer);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be reach", exp);
            }
        }
    }
}
