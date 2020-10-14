using IntroToAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync("http https://swapi.dev/api/people/1/").Result;

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Person personResponse = response.Content.ReadAsAsync<Person>().Result;
                Console.WriteLine(personResponse.Name);

                foreach(string vehicleURL in personResponse.Vehicles)
                {
                    HttpResponseMessage vehicleResponse = httpClient.GetAsync(vehicleURL).Result;
                    Console.WriteLine(vehicleResponse.Content.ReadAsStringAsync().Result);
                    Vehicle vehicle = vehicleResponse.Content.ReadAsAsync<Vehicle>().Result;
                    Console.WriteLine(vehicle.Name);

                }
            }

            SWAPIService swapiService = new SWAPIService();
            Person personOne = swapiService.GetPersonAsync("https://swapi.dev/api/people/11").Result;

            if(personOne != null)
            {
                Console.Clear();
                Console.WriteLine($"The character that has been entered is: {personOne.Name}");
                foreach(string vehicleUrl in personOne.Vehicles)
                {
                    var vehicle = swapiService.GetVehicleAsync(vehicleUrl).Result;
                    Console.WriteLine($"They drive: {vehicle.Name}");
                }
            }
        }
    }
}
