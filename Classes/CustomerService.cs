using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCustomerInvitaion.Classes
{/// <summary>
/// responsible for reading customer list from file and filtering
/// </summary>
    public class CustomerService
    {
        /*
         Office  Latitude and Longitude
         */
        private const double officeLatitude = 53.339428;
        private const double officeLongitude = -6.257664;  
        /*
         default file URL
         */
        private const string DataURL = "/Data/customers.txt";


        /// <summary>
        /// read data from the text file and filter it based on 100 km distance
        /// </summary>
        /// <param name="fileName">optional file name if want to use different source file</param>
        /// <returns>filtered customer list</returns>
        public List<Customer> GetList(string? fileName)
        {
            var customerList = new List<Customer>();
            string  currentDirectory="", path="";

            //prepare reading path
            if (string.IsNullOrEmpty(fileName))
            {
                //use default value
                 currentDirectory = Directory.GetCurrentDirectory();
                 path = currentDirectory + DataURL;              
            }
            else
            {
                //use diffrent file 
                 currentDirectory = Directory.GetCurrentDirectory();
                 path = currentDirectory + fileName;
            } 
            //read file
            customerList = ReadFileContent(path);

            //filter list based on distance
            return customerList
                .Where(e => CalculateDistance(e.Longitude, e.Latitude))
                .OrderBy(e=>e.UserId)
                .ToList();
             
        }

        /// <summary>
        /// read file
        /// </summary>
        /// <param name="filePath">path of text</param>
        /// <returns>list of customer in the file</returns>
        public List<Customer> ReadFileContent(string filePath)
        {
            List<Customer> list = new List<Customer>();
            try
            {
                if (File.Exists(filePath))
                {                 
                    var line = "";
                    StringBuilder sbText = new StringBuilder();

                    //read line by line and convert to customer object
                    using (var reader = new System.IO.StreamReader(filePath))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            var customer = JsonConvert.DeserializeObject<Customer>(line);
                            list.Add(customer);
                        }
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      
        /// <summary>
        /// calculate distance of a point from office
        /// </summary>
        /// <param name="longitude">longitude</param>
        /// <param name="latitude">latitude</param>
        /// <returns>ture if the distance is less than 100</returns>
        public bool CalculateDistance(double longitude, double latitude)
        {
            var rad = Math.PI / 180;
            var earthRadius = 6371;
            var deltaLon = Math.Abs(officeLongitude * rad - longitude * rad);

            var h = Math.Acos(Math.Sin(officeLatitude * rad) * Math.Sin(latitude * rad)
                          + (Math.Cos(officeLatitude * rad) * Math.Cos(latitude * rad) * Math.Cos(deltaLon)))
                      * earthRadius;

            return (h <= 100);
          
        }
    }
}
