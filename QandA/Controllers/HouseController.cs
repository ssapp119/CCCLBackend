using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QandA.Data;
using QandA.Models;
using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

namespace QandA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HouseController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        
        public HouseController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public HouseModel Get(string address)
        {
            IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            string result = "";
            if (remoteIpAddress != null)
            {
                // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                // This usually only happens when the browser is on the same machine as the server.
                if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
            .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                }
                result = remoteIpAddress.ToString();
            }

            if (result == "104.222.81.113" || result == "208.126.160.130")
                {
                    return new HouseModel { Address = "BLOCKED" };
                }


            // ip = Request.HttpContext.Connection.RemoteIpAddress;

           // string userRequest = System.Web.HttpContext.Current.Request.UserHostAddress;


            var house = _dataRepository.GetHouse(address);

            return house;
            //return new HouseModel();
           // return house;

          //  var house = _dataRepository.HouseExists
           /* return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
               // Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
    }
}
