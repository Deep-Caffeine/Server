using System;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{
    private readonly ILogger<HelloWorldController> mLogger;

    public HelloWorldController(ILogger<HelloWorldController> logger)
    {
        mLogger = logger;
    }

    [HttpGet]
    public string Get()
    {
        mLogger.LogInformation($"Hello");

        return "Hello, World!";
    }
}

