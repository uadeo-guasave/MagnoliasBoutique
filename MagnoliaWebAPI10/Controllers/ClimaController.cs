using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagnoliaWebAPI10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClimaController : ControllerBase
{
    // localhost:5001/api/Clima/weatherforecast
    [HttpGet("weatherforecast")]
    public ActionResult<WeatherForecast[]> Get()
    {
        var summaries = new[]
        {
            "Helado", "Frío intenso", "Frío", "Fresco", "Templado", "Cálido", "Agradable", "Caluroso", "Sofocante", "Abrasador"
        };

        var forecast =  Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }
}


public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}