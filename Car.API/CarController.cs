using Microsoft.AspNetCore.Mvc;

namespace Car.API;

[ApiController]
[Route("api/[Controller]")]
public class CarController:ControllerBase
{
    [HttpPost]
    public string  CarApi()
    {
        return "Here is Car Controller";
    }
}
