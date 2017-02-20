using FanAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FanAPI
{
    class Program
    {
        private static HttpClient client = new HttpClient();

        private static void SetUpClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("http://www.anapioficeandfire.com/api/");
        }

        //static List<Character> CharacterList()
        //{
        //    var response = client.GetAsync("characters");
        //}

        static Character GetCharacter(string id)
        {
            var response = client.GetAsync($"characters/{id}").Result;
            return response.Content.ReadAsAsync<Character>().Result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ASOIAF API");
            SetUpClient();
            var jon = GetCharacter("583");
            List<House> jonshouses = jon.AllegianceDetail(client);
            foreach (House house in jonshouses)
            {
                Console.WriteLine(house.Name);
            }
        }
    }
}
