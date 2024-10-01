using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QandA.Data;
using QandA.Models;
namespace QandA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ScheduleJobController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        
        public ScheduleJobController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpPost]
        public int PostJobSchedule(ScheduledJobModel model)
        {
            int id = 0;
            
            id = _dataRepository.PostScheduleJob(model);

            return id;
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
