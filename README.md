# APBD CW5 Web API

Projekt ASP.NET Core Web API przygotowany w ramach ćwiczenia 5.

## Opis
Aplikacja umożliwia zarządzanie salami oraz rezerwacjami centrum szkoleniowego.  
Dane są przechowywane wyłącznie w pamięci aplikacji w statycznych listach.

## Zakres funkcjonalności
- pobieranie wszystkich sal
- pobieranie sali po id
- filtrowanie sal po budynku i parametrach query
- dodawanie, edycja i usuwanie sal
- pobieranie wszystkich rezerwacji
- pobieranie rezerwacji po id
- filtrowanie rezerwacji po dacie, statusie i sali
- dodawanie, edycja i usuwanie rezerwacji
- walidacja danych wejściowych
- obsługa konfliktów czasowych rezerwacji

## Technologie
- C#
- ASP.NET Core Web API

## Uruchomienie
```bash
dotnet restore
dotnet run
