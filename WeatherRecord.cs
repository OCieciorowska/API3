using Microsoft.EntityFrameworkCore;
namespace API3;
//struktura pojedynczego rekordu pogodowego
public class WeatherRecord
{
    public int Id { get; set; }// unikalny identyfikator rekordu klucz główny w bazie
    public required string City { get; set; }//required – oznacza, że ta wartość musi być ustawiona (C# 11+)
    public required double Temperature { get; set; }//double bo moga byc ulamkowe
    public required double Pressure { get; set; }
    public required double Humidity { get; set; }
}