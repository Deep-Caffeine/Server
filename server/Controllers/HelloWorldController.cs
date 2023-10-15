using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{
    private readonly ILogger<HelloWorldController> _logger;

    public HelloWorldController(ILogger<HelloWorldController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public CreatedResult Get()
    {
        _logger.LogInformation($"Hello");
        
        /*
         * URI 부분은 Response Header의 Location에 담기고
         * Value 부분은 Response Body에 입력된다.
         */ 
        return Created("test", new
        {
            message = "testResponseMessage"
        });
    }
}

