using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Request
    {
        public Request(DateTime leaseBegin, DateTime leaseLast, int userID, int vehicleID)
        {
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
            UserID = userID;
            VehicleID = vehicleID;
        }

        public int RID { get; set; }
        public DateTime LeaseBegin { get; set; }
        public DateTime LeaseLast { get; set; }
        public int UserID { get; set; }
        public int VehicleID { get; set; }
        public int StatusID { get; set; }
    }
}
