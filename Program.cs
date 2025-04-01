namespace API3;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Program się uruchomił!");
        Console.Write("Podaj nazwę miasta: ");
        string city = Console.ReadLine(); // Wczytanie nazwy miasta
        Console.WriteLine($"Wpisano: {city}");

        // Pobranie danych pogodowych z API
        var weatherData = await WeatherAPI.GetWeatherAsync(city);

        // Tworzenie nowego obiektu rekordu pogodowego
        var weatherRecord = new WeatherRecord
        {
            City = city,
            Temperature = weatherData.Temperature,
            Pressure = weatherData.Pressure,
            Humidity = weatherData.Humidity
        };

        using var dbContext = new WeatherDbContext();
        try
        {
            dbContext.WeatherRecords.Add(weatherRecord); // Dodanie rekordu do bazy
            Console.WriteLine(" Dodano rekord do bazy danych.");
            dbContext.SaveChanges(); // Zapisanie zmian
            Console.WriteLine(" Zapisano do bazy");
        }
        catch (Exception ex)
        {
            Console.WriteLine(" Błąd: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("🔍 Szczegóły: " + ex.InnerException.Message);
            }
        }
    }
}
