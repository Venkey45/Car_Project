using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Project.Models
{
    public class Model
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string password { get; set; }

        public string Email { get; set; }

        public long phone { get; set; }
        public string Email_OTP { get; set; }
       
        public int Booking_id { get; set; }
        public string B_Name { get; set; }
        public long _Phone { get; set; }
        public string _Email { get; set; }
        public string Model_Type { get; set; }
        public double price { get; set; }
        public string time { get; set; }
      
            public string DeloreanModel { get; set; }
            public decimal Deloreanprice { get; set; }
        
    }
  


}
