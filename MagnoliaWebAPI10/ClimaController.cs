using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagnoliaWebAPI10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClimaController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Hola desde el endpoint GET de Clima";
    }
}
