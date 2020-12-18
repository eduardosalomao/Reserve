using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace COVID.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        [HttpGet("[action]")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IEnumerable<Countries> CovidList()
        {
            var client = new RestClient("https://api.covid19api.com/summary");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            CountriesCovid weatherForecast = JsonConvert.DeserializeObject<CountriesCovid>(response.Content);

            int position = 1;
            weatherForecast.Countries = weatherForecast.Countries.OrderByDescending(o => o.TotalAtivedCalculated).Take(10).ToList();

            weatherForecast.Countries = weatherForecast.Countries.Select(s => new Countries() { Country = s.Country, Position = position++
            , TotalAtived = s.TotalAtivedCalculated }).ToList();

            return weatherForecast.Countries;
        }

        public class CountriesCovid
        {
            public string Message { get; set; }
            public List<Countries> Countries { get; set; }
            
        }

        public class Countries
        {
            public string Country { get; set; }
            public int Position { get; set; }
            public int TotalAtivedCalculated { get { return TotalConfirmed - TotalRecovered; } }
            public int TotalAtived { get; set; }
            public int TotalConfirmed { get; set; }
            public int TotalRecovered { get; set; }

        }
    }
}
