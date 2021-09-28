using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Customer
    {
        public Customer(int id, string firstName, string lastName, string email, string phoneNo, DateTime dateOfBirth)
        {
            CID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNo = phoneNo;
            DateOfBirth = dateOfBirth;
        }

        public int CID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
