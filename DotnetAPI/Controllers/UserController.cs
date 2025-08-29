using Microsoft.AspNetCore.Mvc;
// using Microsoft.VisualBasic; 

namespace DotnetAPI.Controllers;
// Brings in ASP.NET Core MVC framework so you can use [ApiController], [Route], ControllerBase, etc.


[ApiController]
[Route("Controller")]

// namespace DotnetAPI.Controllers;

// Groups your controller class logically.

// [ApiController]

// Tells ASP.NET Core this is an API controller (enables automatic model binding, validation, and error handling).

// [Route("Controller")]

// Sets the base route for all endpoints in this controller.

// Important: "Controller" here is a literal string, not a placeholder.

// So your endpoints will start with /Controller/....

// Example: /Controller/GetUsers/someValue

// If you want it dynamic (based on class name), you should write:

// [Route("[controller]")]


// which makes it /User/GetUsers/... because your class is UserController.





public class UserController : ControllerBase
{




    UserController()
    {

    }

    // This is a constructor but it’s currently empty.

    // Typically, you’d use it for Dependency Injection (e.g., inject a service like IUserRepository).

    // Right now, it doesn’t do anything, so you could even remove it.

    [HttpGet("GetUsers/{testValue}")]
    // public IActionResult Test()
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



        // [HttpGet("GetUsers/{testValue}")]

        // This means the route will be:

        // /Controller/GetUsers/{testValue}


        // Example request:

        // GET https://localhost:5001/Controller/GetUsers/Hello


        // public string[] GetUsers(string testValue)

        // ASP.NET Core automatically binds {testValue} from the route to the method parameter testValue.

        // Inside method:

        // Builds a string array with two fixed values ("test1", "test2") and the user-provided testValue.

        // Returns array:

        // [
        //   "test1",
        //   "test2",
        //   "Hello"
        // ]

        // ✅ What Works

        // This controller will run correctly as-is.

        // Sending a GET request with a value will return a JSON array.

        // ⚡ Suggestions

        // Use [Route("[controller]")] instead of "Controller" to make endpoints cleaner:

        // [Route("[controller]")]


        // Then your endpoint becomes:

        // /User/GetUsers/Hello


        // Consider returning IActionResult (more flexible):

        // [HttpGet("GetUsers/{testValue}")]
        // public IActionResult GetUsers(string testValue)
        // {
        //     var responseArray = new[] { "test1", "test2", testValue };
        //     return Ok(responseArray);
        // }


        // This way you can return Ok(...), NotFound(), BadRequest(), etc., depending on conditions.

