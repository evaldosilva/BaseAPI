using BaseAPI.Models.Responses;
using BaseAPI.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace BaseAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AliveController : ControllerBase
{
    private const string ALIVE_REQUEST_INFO = "Is alive requested from {0} to {1}";
    private readonly ILogger<AliveController> _logger;

    public AliveController(ILogger<AliveController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(AliveResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult Get()
    {
        _logger.LogInformation(ALIVE_REQUEST_INFO, HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Host);

        var hardwareInfo = HardwareMonitor.GetStatus();
        AliveResponse aliveResponse = new(Assembly.GetExecutingAssembly().GetName().Name,
                                          Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                                          hardwareInfo);
                
        return Ok(aliveResponse);
    }
}