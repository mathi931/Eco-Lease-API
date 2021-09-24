using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Reservation
    {
        public Reservation(DateTime leaseBegin, DateTime leaseLast, int customerID, int vehicleID)
        {
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
            CustomerID = customerID;
            VehicleID = vehicleID;
        }

        public int RID { get; set; }
        public DateTime LeaseBegin { get; set; }
        public DateTime LeaseLast { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        public int StatusID { get; set; }
    }
}
