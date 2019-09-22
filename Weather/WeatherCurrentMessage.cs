using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class WeatherCurrentMessage
    {
        public coord coord { get; set; }
        public List<weather> weather { get; set; }
        public string @base { get; set; }
        public main main { get; set; }
        public int visibility { get; set; }
        public wind wind { get; set; }
        public clouds clouds { get; set; }
        public UInt64 dt { get; set; }
        public sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
    public class coord
    {
        public decimal lon { get; set; }
        public decimal lat { get; set; }
    }

    public class weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class main
    {
        public decimal temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public decimal temp_min { get; set; }
        public decimal temp_max { get; set; }
    }

    public class wind
    {
        public decimal speed { get; set; }
        public int deg { get; set; }
        public decimal gust { get; set; }
    }

    public class clouds
    {
        public int all { get; set; }
    }

    public class sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public decimal message { get; set; }
        public string country { get; set; }
        public UInt64 sunrise { get; set; }
        public UInt64 sunset { get; set; }
    }
}
