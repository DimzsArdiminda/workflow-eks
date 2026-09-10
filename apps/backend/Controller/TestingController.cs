using Microsoft.AspNetCore.Mvc;
using workflowEkstensi.backend.Services;

namespace apps.backend.Controller;
[ApiController]

public class TestingController : ControllerBase
{
    private readonly TestingServices _testingServices;

    public TestingController(TestingServices testingServices)
    {
        _testingServices = testingServices;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return Ok(_testingServices.GetMessage());
    }

    [HttpGet("/greeting/{name}")]
    public IActionResult Greeting(string name)
    {
        return Ok(_testingServices.GetGreeting(name));
    }

    [HttpPost("/api/palindrom")]
    public IActionResult CheckPalindrome([FromBody] int Input)
    {
        return Ok(_testingServices.CheckPalindrome(Input));
    }
}
