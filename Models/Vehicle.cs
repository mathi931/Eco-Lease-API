using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Vehicle
    {
        public int VId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime Registered { get; set; }
        public string PlateNo { get; set; }
        public int Km { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"{Make} {Model} {Registered} {PlateNo} {Km}{Notes} {Status}";
        }
    }
}
