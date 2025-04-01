
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace API3;
//obsługa zapytania API
class WeatherAPI
{
    private static readonly HttpClient client = new HttpClient(); //Tworzy jedno połączenie HTTP (oszczędza zasoby).
    private const string apiKey = "6bf5480812943946635562298e42df3d"; // Klucz API do uwierzytelnienia w OpenWeatherMap.

    public static async Task<WeatherRecord> GetWeatherAsync(string city)
    {
        using (var db = new WeatherDbContext())
        {
            // Sprawdzamy, czy mamy dane w bazie
            var existingRecord = db.WeatherRecords.FirstOrDefault(w => w.City == city);
            if (existingRecord != null)
            {
                Console.WriteLine("Dane pobrane z bazy danych.");
                return existingRecord;
            }
        }

        // Pobranie danych z API
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
        HttpResponseMessage response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Błąd API: {response.StatusCode}");
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Odpowiedź API: " + jsonResponse);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        WeatherData weather = JsonSerializer.Deserialize<WeatherData>(jsonResponse, options);

        
        if (weather == null)
        {
            throw new Exception("Błąd: Deserializacja zwróciła null!");
        }

        if (weather.main == null)
        {
            throw new Exception("Błąd: Brak sekcji 'main' w odpowiedzi API!");
        }

        // SPRAWDZENIE, czy API zwróciło poprawne dane
        if (weather == null || string.IsNullOrEmpty(weather.Name))
        {
            throw new Exception("Błąd: Nie udało się pobrać danych pogodowych dla podanego miasta.");
        }

        var weatherRecord = new WeatherRecord
        {
            City = weather.Name, // Upewnij się, że nie jest NULL
            Temperature = weather.main.temp,
            Pressure = weather.main.pressure,
            Humidity = weather.main.humidity
        };

        using (var db = new WeatherDbContext())
        {
            db.WeatherRecords.Add(weatherRecord);
            db.SaveChanges();
        }

        return weatherRecord;
    }
}