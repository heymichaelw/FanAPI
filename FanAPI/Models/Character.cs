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
        public Uri URL { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public List<Uri> Allegiances { get; set; }
        public List<Uri> Books { get; set; }
        
        public List<House> AllegianceDetail(HttpClient client)
        {
            List<House> allegiances = new List<House>();
            foreach (Uri alliance in Allegiances)
            {
                var response = client.GetAsync(alliance).Result;
                House allegiance = response.Content.ReadAsAsync<House>().Result;
                allegiances.Add(allegiance);
            }
            return allegiances;
        }

        public List<Book> BookDetail(HttpClient client)
        {
            List<Book> books = new List<Book>();
            foreach (Uri novel in Books)
            {
                var response = client.GetAsync(novel).Result;
                Book b = response.Content.ReadAsAsync<Book>().Result;
                books.Add(b);
            }
            return books;
        }
        
    }
}
