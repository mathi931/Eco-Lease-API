using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Request
    {
        public Request(int userID, int vehicleID)
        {
            UserID = userID;
            VehicleID = vehicleID;
        }

        public int RID { get; set; }
        public int UserID { get; set; }
        public int VehicleID { get; set; }
        public int StatusID { get; set; }
    }
}
