using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Services.CouponAPI.Data;
using MicroService.Services.CouponAPI.Model;
using Microsoft.Extensions.Logging;

namespace MicroService.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //api/

    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;

       
        //private readonly ILogger<WeatherForecastController> _logger;

        public CouponApiController(AppDbContext db)
        {
            _db = db;
        }

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public IEnumerable<Coupon> Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                return objList;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public Coupon Get(int id)
        {
            try
            {
                return _db.Coupons.FirstOrDefault(s=>s.CouponId == id);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(index),
        //            TemperatureC = rng.Next(-20, 55),
        //            Summary = Summaries[rng.Next(Summaries.Length)]
        //        })
        //        .ToArray();
        //}
    }
}
