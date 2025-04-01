using Microsoft.EntityFrameworkCore;
namespace API3;

public class WeatherRecord
{
    public int Id { get; set; }
    public required string City { get; set; }
    public required double Temperature { get; set; }
    public required double Pressure { get; set; }
    public required double Humidity { get; set; }
}