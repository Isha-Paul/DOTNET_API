var builder = WebApplication.CreateBuilder(args);

// 1. Create the WebApplication builder
// Bootstraps your application.

// Loads configuration (appsettings.json, env vars, etc.).

// Prepares Dependency Injection (DI) container.

// Gives you builder.Services to register services.





builder.Services.AddControllers();

// 2. Register Services
// Adds support for MVC Controllers (like UserController, WeatherForecastController).






// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();


//Added by me
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// AddEndpointsApiExplorer() → lets Swagger scan your endpoints.

// AddSwaggerGen() → generates Swagger/OpenAPI documentation.

// ✅ This is why you’ll get Swagger UI at /swagger when running in development.





var app = builder.Build();
// Creates the actual WebApplication object with everything configured.



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    //Added by me
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

//Added by me
else
{
    app.UseHttpsRedirection();
}

// if (app.Environment.IsDevelopment()) → checks if you are in Development environment.

// In Development:

// UseSwagger() → generates the Swagger JSON at /swagger/v1/swagger.json.

// UseSwaggerUI() → enables interactive UI at /swagger.

// In Production (else):

// UseHttpsRedirection() → automatically redirects http:// → https://.



// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");
// .WithOpenApi();




//added by me

app.MapControllers();
// Registers your controller routes (UserController, WeatherForecastController, etc.).

// Example: /User/GetUsers/{testValue} becomes available.



app.Run();

// Starts the web server (Kestrel by default).

// Keeps the app running and listening for requests.



// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
