using Microsoft.AspNetCore.Mvc;

namespace Plane.API;

[ApiController]
[Route("api/[Controller]")]
public class PlaneController : ControllerBase
{
    [HttpPost]
    public string PlaneApi()
    {
        return "Here is Plane Controller";
    }
}


