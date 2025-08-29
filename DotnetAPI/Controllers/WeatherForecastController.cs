using Microsoft.AspNetCore.Mvc;
// using Microsoft.VisualBasic; 

namespace DotnetAPI.Controllers;

// Groups your controller class into a namespace (DotnetAPI.Controllers).

[ApiController]

// Attribute that tells ASP.NET Core this is an API controller.

// It provides helpful features like automatic request validation and consistent responses for bad requests.


[Route("Controller")]

// Defines the route template for this controller.

// "Controller" is a literal string, not a placeholder.

// That means your endpoint will be /Controller (not /WeatherForecast as in default templates).

// If you wanted it dynamic, you’d usually write:

// [Route("[controller]")]


// which automatically replaces [controller] with the class name minus "Controller" (WeatherForecast → /WeatherForecast).


public class WeatherForecastController : ControllerBase
{
    //     ControllerBase is the base class for API controllers (without Razor Views).

    // Your WeatherForecastController now serves as a REST API endpoint provider.
    

    private readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet("", Name = "GetWeatherForecast")]
    //     This tells ASP.NET Core this method responds to HTTP GET requests at:

    // /Controller (because of [Route("Controller")])

    // Name = "GetWeatherForecast" just gives this route an internal name (useful if you want to reference it later when generating URLs).


    public IEnumerable<WeatherForecast> GetFiveDayForecast()   //Returns a collection of WeatherForecast objects (5 items).
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            _summaries[Random.Shared.Next(_summaries.Length)]
        ))
        .ToArray();

            // Enumerable.Range(1, 5) → generates numbers 1 through 5.

            // For each number (index), it creates a new WeatherForecast:

            // DateOnly.FromDateTime(DateTime.Now.AddDays(index)) → forecast for today + 1..5 days.

            // Random.Shared.Next(-20, 55) → random temperature between -20°C and 55°C.

            // _summaries[...] → random weather condition string from the array.

            // .ToArray() converts it into an array.



        return forecast;
    }


}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// record → introduced in C# 9, an immutable reference type designed for data models.

// Properties:

// Date → forecast date (type: DateOnly).

// TemperatureC → temperature in Celsius.

// Summary → description (nullable string).

// Extra computed property:

// TemperatureF → derived Fahrenheit value from Celsius.
