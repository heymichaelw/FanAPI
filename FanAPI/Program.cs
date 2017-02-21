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

        static List<Character> GetAll(int pagenumber)
        {
            var response = client.GetAsync($"characters?page={pagenumber}").Result;
            List<Character> allcharacters = response.Content.ReadAsAsync<List<Character>>().Result;
            return allcharacters;
        }


        static Character GetCharacter(string id)
        {
            var response = client.GetAsync($"characters/{id}").Result;
            return response.Content.ReadAsAsync<Character>().Result;
        }

        private static void GetCharacterDetails(string id)
        {
            var character = GetCharacter(id);
            Console.WriteLine(character.Name);
            Console.WriteLine(character.Gender);
            List<House> characterhouses = character.AllegianceDetail(client);
            List<Book> characterbooks = character.BookDetail(client);
            foreach (House house in characterhouses)
            {
                Console.WriteLine(house.Name);
            }
            foreach (Book book in characterbooks)
            {
                Console.WriteLine(book.Name);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ASOIAF API");
            SetUpClient();
            MainMenu();
        }

        private static void CharacterList()
        {
            for (int i = 1; i < 215;)
            {
                var characters = GetAll(i);
                foreach (var character in characters)
                {
                    string path = character.URL.ToString();
                    int pos = path.LastIndexOf("/") + 1;
                    Console.WriteLine(" ");
                    Console.WriteLine((path.Substring(pos, path.Length - pos)) + " " + character.Name);
                }
                Console.WriteLine("Character Details: [number]");
                Console.WriteLine("[N]ext / [P]revious / [M]enu");
                var choice = Console.ReadLine();
           
                switch (choice)
                {
                    case "N":
                        i++;
                        break;
                    case "P":
                        i--;
                        break;
                    case "M":
                        MainMenu();
                        break;
                    default:
                        GetCharacterDetails(choice);
                        break;
                }

            }
        }

        private static void MainMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("View [C]haracter List");
            var selection = Console.ReadLine().ToUpper();
            switch (selection)
            {
                case "C":
                    CharacterList();
                    break;
            }
        }
    }
    }

