using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FanAPI.Models
{
    class Character
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public Uri Allegiances { get; set; }
        public Uri Books { get; set; }

        public House AllegianceDetail(HttpClient client)
        {
            var response = client.GetAsync(Allegiances).Result;
            House allegiance = response.Content.ReadAsAsync<House>().Result;
            return allegiance;
        }
    }
}
