using ApiProject_Weather.Context;
using ApiProject_Weather.Entitys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace ApiProject_Weather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController : ControllerBase
    {
       WeatherContext context = new WeatherContext();

        [HttpGet]
        public  IActionResult WeatherCityList()
        {
            var values = context.Cities.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateWeatherCity(City city)
        {  context.Cities.Add(city);
                context.SaveChanges();
                return Ok("Şehir Eklendi");  
        }
       

        
        [HttpDelete]
        public IActionResult DeleteWeatherById(int id)
        {
            var value = context.Cities.Find(id);

            if (value == null)
                return NotFound($"{id} ID'li şehir bulunamadı.");

            context.Cities.Remove(value);
            context.SaveChanges();

            return Ok($"{id} ID'li şehir başarıyla silindi.");
        }

        [HttpPut]

        public IActionResult UpdateWeatherCity(City city) {

            var values = context.Cities.Find(city.CityId);
            values.CityName = city.CityName;
            values.Country = city.Country;
            values.Temprature = city.Temprature;
            values.Detail = city.Detail;    
            context.SaveChanges();
            return Ok("Başarıyla Güncellendi");
        
        }

        [HttpGet("GetWeatherCity")]

        public IActionResult GetWeatherCity()
        {
            var value = context.Cities.Select(a => a.CityName).ToList();
            return Ok(value);

        }

        [HttpGet("TotalGetCityCount")]
        public IActionResult TotalGetCityCount()
        {
            var value = context.Cities.Count();
            return Ok(value);
        }
        [HttpGet("MaxTempCityName")]

        public IActionResult MaxTempCityName()
        {
            var value = context.Cities.OrderByDescending(a => a.Temprature).Select(a => a.CityName).FirstOrDefault();
            return Ok(value);

        }








    }
}
