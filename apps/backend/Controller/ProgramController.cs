using Microsoft.AspNetCore.Mvc;
using workflowEkstensi.backend.Services;

namespace apps.backend.Controller;
[ApiController]

public class ProgramController : ControllerBase
{
    private readonly ProgramServices _programServices;

    public ProgramController(ProgramServices programServices)
    {
        _programServices = programServices;
    }

    [HttpGet("/api/protocol/")]
    public IActionResult protocol()
    {
        return Ok(_programServices.protocol());
    }
}