using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarWebApp.Models
{
    public class CurrencyConvert
    {
        public static double ExchangeRate(String from)
        {
            string url = "https://free.currconv.com/api/v7/convert?q=" + from + "_GEL&compact=ultra&apiKey=721fa777a96e9eb4d7a5";

            var jsonString = GetResponse(url);
            var j = JsonConvert.DeserializeObject<dynamic>(jsonString); Console.WriteLine();
            return j[$"{from}_GEL"];
        }

        private static string GetResponse(string url)
        {
            string jsonString;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }
    }
}
