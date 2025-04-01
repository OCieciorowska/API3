using Microsoft.EntityFrameworkCore;
namespace API3;
//klasa reprezentuje kontekst bazy danych
public class WeatherDbContext : DbContext
{
    public DbSet<WeatherRecord> WeatherRecords { get; set; }//tabela obiektów WeatherRecords w bazie danych 
//konfiguracja połączenia z baza
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(@"Data Source=WeatherData.db");
    }
//konstruktor- tworzy baze danych jesli jej nie ma
    public WeatherDbContext()
    {
        Database.EnsureCreated();
    }
}