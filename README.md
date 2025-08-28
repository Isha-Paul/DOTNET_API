# DOTNET_API


1. Program.cs

This is the entry point of your Web API application.

var builder = WebApplication.CreateBuilder(args);


Creates a WebApplicationBuilder object.

It loads configuration (from appsettings.json, environment vars, etc.) and prepares dependency injection.

Adding Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


AddControllers() → Registers controller support in the DI container.

AddEndpointsApiExplorer() → Helps generate OpenAPI (Swagger) metadata for minimal APIs and controllers.

AddSwaggerGen() → Adds Swagger generation (for API docs and testing UI).

Build App
var app = builder.Build();


Builds the configured app.

Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}


In development mode:

UseSwagger() → Enables middleware to generate JSON docs.

UseSwaggerUI() → Enables the Swagger web UI.

In production mode:

UseHttpsRedirection() → Redirects all HTTP requests to HTTPS.

Map Controllers
app.MapControllers();
app.Run();


MapControllers() → Maps controller routes ([Route]) to HTTP endpoints.

app.Run() → Starts the web server.

✅ So Program.cs sets up:

Controllers

Swagger (in dev)

HTTPS (in prod)

2. UserController.cs
namespace DotnetAPI.Controllers;

[ApiController]
[Route("Controller")]   // 👈 Problem here
public class UserController : ControllerBase
{
    UserController()
    {
    }

    [HttpGet("GetUsers/{testValue}")]
    public string[] GetUsers(string testValue)
    {
        string[] responseArray = new string[] {
            "test1",
            "test2",
            testValue
        };
        return responseArray;
    }
}

Explanation

[ApiController] → Enables API-specific features (automatic model validation, route binding, etc.).

[Route("Controller")] → ❌ Problem: this literally makes your route /Controller/....

Instead, you normally use [Route("api/[controller]")] so ASP.NET replaces [controller] with User.

That would give endpoint: /api/User/GetUsers/{testValue}.

GetUsers(string testValue) → HTTP GET endpoint:

Example request:
GET /api/User/GetUsers/hello

Response:
["test1", "test2", "hello"].

3. WeatherForecastController.cs
namespace DotnetAPI.Controllers;

[ApiController]
[Route("Controller")]   // 👈 Same issue here
public class WeatherForecastController : ControllerBase
{
    private readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet("", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> GetFiveDayForecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaries[Random.Shared.Next(_summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }
}

Explanation

[Route("Controller")] → ❌ Again makes the route /Controller.
Should be [Route("api/[controller]")], which makes /api/WeatherForecast.

GetFiveDayForecast() → Returns 5 random weather forecasts.

Enumerable.Range(1,5) → Creates 5 elements.

Random.Shared.Next(-20, 55) → Random temperature.

Picks a random summary from _summaries.

Example request:
GET /api/WeatherForecast

Example response:

[
  {
    "date": "2025-08-29",
    "temperatureC": 25,
    "summary": "Mild",
    "temperatureF": 77
  },
  {
    "date": "2025-08-30",
    "temperatureC": 10,
    "summary": "Chilly",
    "temperatureF": 50
  }
]

WeatherForecast record
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


A record type → good for data models (immutable by default).

Auto-implements equality and ToString().

TemperatureF → Calculated property (converted from Celsius).

Summary of Fixes

✅ Your API works but to follow best practices:

Change routes:
[Route("api/[controller]")]


So endpoints become:

/api/User/GetUsers/{testValue}

/api/WeatherForecast
