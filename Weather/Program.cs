using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;


namespace WeatherApp
{
    public class Options
    {
        public string zip { get; set; }
        public string units { get; set; }
        public string type { get; set; }
        public bool IsZipCode(string zipCode)
        {
            var _usZipCode = @"\d{5}$";
            bool valid = false;

            if(Regex.Match(zipCode, _usZipCode).Success)
            {
                valid = true;
            }

            return valid;
        }
    }
    class Program
    {
        static readonly HttpClient http = new HttpClient();

        static void ShowWeather(WeatherCurrentMessage weather)
        {
            Console.Clear();
            Console.WriteLine($"Name: {weather.name}");
        }

        static async Task<WeatherCurrentMessage> GetWeatherAsync(string path)
        {
            WeatherCurrentMessage weather = null;
            var uri = new Uri(path);
            HttpResponseMessage response = await http.GetAsync(uri.AbsoluteUri);

            if (response.IsSuccessStatusCode)
            {
                weather = await response.Content.ReadAsAsync<WeatherCurrentMessage>();
            }

            return weather;
        }

        static async Task RunAsync()
        {
            Options opt = RunMenu();

            string url = GetRequestURL(opt);
            Console.WriteLine($"url: {url}");

            try
            {

                WeatherCurrentMessage msg = await GetWeatherAsync(url);
                ShowWeather(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static string GetRequestURL(Options opt)
        {
            string url = "https://api.openweathermap.org/data/2.5/";
            url += opt.type;
            url += "zip=" + opt.zip;
            
            if(opt.units != "Standard")
            {
                url += "&units=" + opt.units;
            }

            url += "&APPID=" + ConfigurationManager.AppSettings["APIKey"];
            return url;
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static Options RunMenu()
        {
            Options opt = new Options();
            var menu = true;
            string input;

            while (menu)
            {
                Console.Clear();
                Console.WriteLine("Please Enter Zipcode: ");
                Console.WriteLine("---------------");
                Console.WriteLine();
                input = Console.ReadLine();
                if (opt.IsZipCode(input))
                {
                    opt.zip = input;
                    menu = false;
                }
                else
                {
                    Console.WriteLine("INVALID ZIPCODE");
                    Thread.Sleep(2000);
                }
            }

            menu = true;

            while (menu)
            {
                Console.Clear();
                Console.WriteLine("Weather Query: ");
                Console.WriteLine("---------------");
                Console.WriteLine();
                Console.WriteLine("1: Current Weather");
                Console.WriteLine("2: Weather Forecast");
                Console.WriteLine("---------------");
                Console.WriteLine();
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        opt.type = "weather?";
                        menu = false;
                        break;
                    case "2":
                        opt.type = "forecast?";
                        menu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Selection. Please try again.");
                        Thread.Sleep(2000);
                        break;
                }
            }

            menu = true;

            while (menu)
            {
                Console.Clear();
                Console.WriteLine("Weather Units: ");
                Console.WriteLine("---------------");
                Console.WriteLine();
                Console.WriteLine("1: Imperial");
                Console.WriteLine("2: Metric");
                Console.WriteLine("3: Standard");
                Console.WriteLine("---------------");
                Console.WriteLine();
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        opt.units = "Imperial";
                        menu = false;
                        break;
                    case "2":
                        opt.units = "Metric";
                        menu = false;
                        break;
                    case "3":
                        opt.units = "";
                        menu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Selection. Please try again.");
                        Thread.Sleep(2000);
                        break;
                }
            }

            Console.Clear();

            return opt;
        }
    }
}
