using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CountryInfoRetriever
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string countryName = "USA";
            string apiUrl = $"https://restcountries.com/v3.1/name/{countryName}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonResponse = await client.GetStringAsync(apiUrl);
                    
                    var countries = JsonConvert.DeserializeObject<Country[]>(jsonResponse);
                    var country = countries[0];

                    Console.WriteLine($"Country: {country.Name.CommonName}");
                    Console.WriteLine($"Capital: {string.Join(", ", country.Capital)}");
                    Console.WriteLine($"Region: {country.Region}");
                    Console.WriteLine($"Population: {country.Population}");
                }
                catch (JsonReaderException e)
                {
                    Console.WriteLine($"JSON Error: {e.Message}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }
    }

    public class Country
    {
        public Name Name { get; set; }
        public string[] Capital { get; set; }
        public string Region { get; set; }
        public int Population { get; set; }
        // Add other properties as per your requirement
    }

    public class Name
    {
        [JsonProperty("common")]
        public string CommonName { get; set; }
        public string Official { get; set; }
        public Dictionary<string, NativeName> NativeName { get; set; }
    }

    public class NativeName
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }
}