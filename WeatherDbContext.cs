using Microsoft.EntityFrameworkCore;
namespace API3;

public class WeatherDbContext : DbContext
{
    public DbSet<WeatherRecord> WeatherRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(@"Data Source=WeatherData.db");
    }

    public WeatherDbContext()
    {
        Database.EnsureCreated();
    }
}