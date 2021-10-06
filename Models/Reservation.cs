using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Reservation
    {
        public int RID { get; set; }
        public DateTime LeaseBegin { get; set; }
        public DateTime LeaseLast { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
