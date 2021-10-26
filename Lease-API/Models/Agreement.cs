using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Agreement
    {
        public int AID { get; set; }
        public string FileName{ get; set; }
        public Reservation Reservation { get; set; }
    }
}
