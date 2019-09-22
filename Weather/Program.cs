using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WeatherApp
{
    public class Options
    {
        public int zip { get; set; }
        public int units { get; set; }
        public int type { get; set; }
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
        static HttpClient http = new HttpClient();

        static void ShowWeather(WeatherCurrentMessage weather)
        {
            Console.WriteLine($"Name: {weather.name}");
        }

        static async Task<WeatherCurrentMessage> GetWeatherAsync(string path)
        {
            WeatherCurrentMessage weather = null;
            HttpResponseMessage response = await http.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                weather = await response.Content.ReadAsAsync<WeatherCurrentMessage>();
            }

            return weather;
        }

        static async Task RunAsync()
        {
            WeatherCurrentMessage msg = new WeatherCurrentMessage();
            msg = await GetWeatherAsync("");
        }

        static string GetRequestURL(Options opt)
        {
            return "";
        }

        static void Main(string[] args)
        {
        }
    }
}
