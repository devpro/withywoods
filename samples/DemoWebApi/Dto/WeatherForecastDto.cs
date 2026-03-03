namespace Withywoods.DemoWebApi.Dto;

public class WeatherForecastDto
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; init; }

    public string? Summary { get; set; }
}
