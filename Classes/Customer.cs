using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCustomerInvitaion.Classes
{/// <summary>
/// Map text data to customer object
/// </summary>
    public class Customer
    {
        [JsonProperty("latitude")]
        private string stringLatitude { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("longitude")]
        private string stringLongitude { get; set; }

        //convert string to double
        [NotMapped]
        public double Latitude { 
            get { return Double.Parse(stringLatitude); }
        }
        //convert string to double
        [NotMapped]
        public double Longitude
        { 
            get { return Double.Parse(stringLongitude); }
        }

    }
}
