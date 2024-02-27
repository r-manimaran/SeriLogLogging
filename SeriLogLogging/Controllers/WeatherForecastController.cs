using Microsoft.AspNetCore.Mvc;
using Serilog;
using SeriLogLogging;
using ILogger = Serilog.ILogger;

namespace SeriLogLogging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger _logger = Log.ForContext<WeatherForecastController>();

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            // _logger.LogInformation("Starting WeatherForecast Get Method");
            //Comment: Log with Extension method which logs the Class and Method name along with message
            _logger.WithClassAndMethodNames<WeatherForecastController>()
                .Information("Starting WeatherForecast Get Method");

            var rnd = new Random();
            
            try
            {
                if (rnd.Next(0, 5) < 2)
                    throw new Exception("Invalid operration exception.");

                return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray());
            }
            catch(Exception ex)
            {
                //_logger.LogError("{Message} {@Error}", ex.Message, ex);
                return new StatusCodeResult(500);
            }
        }
    }
}
