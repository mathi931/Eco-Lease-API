using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLease_API.Models
{
    public class Vehicle
    {
        public Vehicle(int vId, string make, string model, int registered, string plateNo, int km, string notes, string img, int price, string status)
        {
            VId = vId;
            Make = make;
            Model = model;
            Registered = registered;
            PlateNo = plateNo;
            Km = km;
            Notes = notes;
            Img = img;
            Price = price;
            Status = status;
        }
        public Vehicle()
        {

        }
        public int VId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Registered { get; set; }
        public string PlateNo { get; set; }
        public int Km { get; set; }
        public string Notes { get; set; }
        public string Img { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"{Make} {Model} {Registered} {PlateNo} {Km}{Notes} {Status}";
        }
    }
}
