using Microsoft.AspNetCore.Mvc;
using CustomerManager.Models;

namespace CustomerManager.Controllers;

[ApiController]
[Route("")]
public class HealthController : ControllerBase
{
    [HttpGet("health")]
    public ActionResult<HealthResponse> Health()
    {
        var response = new HealthResponse
        {
            Status = "Healthy",
            Message = "Legacy API is running",
            Timestamp = DateTime.UtcNow
        };

        return Ok(response);
    }
}
