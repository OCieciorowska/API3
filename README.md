# 🌤️ API3 - Konsolowa aplikacja pogodowa w C#

## Opis
Prosta aplikacja konsolowa, która sprawdza aktualną pogodę dla podanego miasta. Dane są pobierane z serwisu OpenWeatherMap i zapisywane w lokalnej bazie danych.

## Technologie

- C# / .NET
- Entity Framework Core
- SQLite
- OpenWeatherMap API

---

##  Instalacja i uruchomienie

### 1. Sklonuj repozytorium


git clone https://github.com/OCieciorowska/API3.git
cd API3

### 1. Instalacja niezbędnych pakietów


dotnet add package Microsoft.EntityFrameworkCore


dotnet add package Microsoft.EntityFrameworkCore.Sqlite




## Aplikacja:
<ul>
<li>Pobiera temperaturę, ciśnienie i wilgotność dla wybranego miasta za pomocą połączenia API</li> 
<li>Zapisuje wyniki w bazie danych</li> 
<li>Pokazuje pogodę w przejrzysty sposób w konsoli</li>
</ul>

## Struktura projektu:
<ul>
<li>WeatherAPI.cs- Obsługa zapytania do API i zapis do bazy</li> 
<li>WeatherDbContext.cs- Kontekst bazy danych, konfiguracja bazy danych SQLite</li> 
<li>Program.cs - Punkt wejściowy aplikacji. Główna logika.</li>
<li> WeatherRecord.cs-Model danych w bazie danych, struktura pojedynczego rekordu pogodowego</li>
  <li>WeatherData.cs-Model danych pogodowych, definiuje strukturę, który przechowuje dane o pogodzie pobrane z API</li>
</ul>

## Przykład działania:

Program się uruchomił!


Podaj nazwę miasta: Kraków


Wpisano: Kraków


Odpowiedź API: {...}


Dane pogodowe:


 Miasto: Krakow
 
 Temperatura: 21.3°C
 
 Ciśnienie: 1015 hPa
 
 Wilgotność: 56%
 
Rekord został dodany do bazy!

## Gdzie są zapisywane dane?

Aplikacja tworzy plik WeatherData.db (baza SQLite) w folderze projektu. Baza danych ma jedną tabelę WeatherRecord, gdzie zapisywane jest miasto i dane pogodowe dla niego.

## Autor

Twórca projektu: Aleksandra Cieciorowska 275412

