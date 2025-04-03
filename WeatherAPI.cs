
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

    public static async Task<WeatherRecord> GetWeatherAsync(string city)//GetWeatherAsync(city) pobiera dane pogodowe dla podanego miasta.
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
        HttpResponseMessage response = await client.GetAsync(url);// await- Wysyła asynchroniczne żądanie HTTP (nie blokuje programu).

        if (!response.IsSuccessStatusCode)//obsługa błędów API
        {
            throw new Exception($"Błąd API: {response.StatusCode}");
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();//odczyt json ii deserializacja
        Console.WriteLine("Odpowiedź API: " + jsonResponse);//Wyświetlam surową odpowiedź było  pomocne w debugowaniu).

        var options = new JsonSerializerOptions //ignoruje wielkość liter w nazwach pól json
        {
            PropertyNameCaseInsensitive = true
        };

        WeatherData weather = JsonSerializer.Deserialize<WeatherData>(jsonResponse, options);//deserializacja

        
        if (weather == null)//sprawdzamy czy sie udała deserializacja
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
//  Wyświetlenie danych w terminalu po deserializacji w terminalu
        Console.WriteLine(" Dane pogodowe:");
        Console.WriteLine($" Miasto: {weather.Name}");
        Console.WriteLine($" Temperatura: {weather.main.temp}°C");
        Console.WriteLine($" Ciśnienie: {weather.main.pressure} hPa");
        Console.WriteLine($" Wilgotność: {weather.main.humidity}%");
        
        var weatherRecord = new WeatherRecord//tworzymy obiekt i wypełniamy danymi z Api
        {
            City = weather.Name, // Upewnij się, że nie jest NULL
            Temperature = weather.main.temp,
            Pressure = weather.main.pressure,
            Humidity = weather.main.humidity
        };
/*
        using (var db = new WeatherDbContext())//tworzymy nowa instancje
        {
            db.WeatherRecords.Add(weatherRecord);//dodajemy rekord do bazy
            db.SaveChanges();//zapisuje zmiany
        }

        return weatherRecord;*/
        try
        {
            using (var db = new WeatherDbContext())
            {
                db.WeatherRecords.Add(weatherRecord);
                int result = db.SaveChanges();
        
                if (result > 0)
                    Console.WriteLine("Rekord został dodany do bazy!");
                else
                    Console.WriteLine("Nie udało się dodać rekordu!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu do bazy: {ex.Message}");
        }

        return weatherRecord;
    }

}