using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalCRUD.Models
{
    public class AdvertViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AdvertOwner { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }

        public Nullable<int> Country_Id { get; set; }
        public string CountryName { get; set; }
        public Nullable<int> State_Id { get; set; }
        public string StateName { get; set; }

        public string CategoryName { get; set; }
        public Nullable<int> CategoryId { get; set; }

        public string Date { get; set; }

        public Nullable<int> CarDetID { get; set; }
        public Nullable<int> HomeDetID { get; set; }
        public Nullable<int> WorkDetID { get; set; }

        public Nullable<int> RoomCount { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<int> Balcony { get; set; }
        public string HeatingSystem { get; set; }

        public string Brand { get; set; }
        public string CarModel { get; set; }
        public string Gear { get; set; }
        public Nullable<int> ModelYear { get; set; }
        public string Color { get; set; }
        public string FuelType { get; set; }

        public string Type { get; set; }
        public string Status { get; set; }
        public string WorkAge { get; set; }
        public string WorkRoom { get; set; }
  
    }

}