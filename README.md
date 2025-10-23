# Dokumenty – Aplikacja rekrutacyjna (.NET + Vue)

## Opis projektu

Projekt został stworzony jako zadanie rekrutacyjne i stanowi kompletną aplikację typu client–server, umożliwiającą zarządzanie dokumentami oraz ich pozycjami. System zapewnia pełną obsługę CRUD, sortowanie, filtrowanie, paginację oraz import danych z plików CSV.

## Uruchamianie projektu

#### Przed uruchomieniem projektu upewnij się, że masz zainstalowane:

- [.NET 8 SDK+](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js (z npm)](https://nodejs.org/en/download)
- [Microsoft SQL Server](https://www.microsoft.com/pl-pl/sql-server/sql-server-downloads) - SQL Server 2022 Developer
- [Visual Studio Code](https://code.visualstudio.com/) – projekt był tworzony i uruchamiany w tym edytorze
- (opcjonalnie) [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/ssms/install/install) – do podglądu danych w bazie

#### Instrukcja Uruchomienia

#### Backend

Najpierw upewnij się, że w pliku **/server/appsettings.json** jest poprawny connection string służący do połączenia aplikacji z bazą danych.

Następnie folderze **/server** odpalić następujące komendy:

```bash
#pobranie paczek
dotnet restore

# zainstalowanie Entity Framework Core Tools jeśli nie ma
dotnet tool install --global dotnet-ef

# tworzenie modelu bazy
dotnet ef database update

# uruchamianie aplikacji
dotnet run
```

Dokumentacja endpointów (Swagger) jest dostępna pod adresem: http://localhost:5190/swagger/

#### Frontend

W folderze **/client** odpalić następujące komendy:

```bash
# instalowanie paczek
npm install

# uruchamianie aplikacji
npm run dev
```

Aplikacja frontendowa dostępna jest pod adresem: http://localhost:5173/

## Technologie

### Backend:

- ASP.NET Core 8 (Web API)
- Entity Framework Core
- CSVHelper
- Microsoft SQL Server

### Frontend:

- Vue 3
- TypeScript
- Pinia
- Axios
- TailwindCSS
- Vue Toastification
- oh-vue-icons

## Główne funkcjonalności

### Zarządzanie dokumentami

- Lista dokumentów w formie tabeli.
- Sortowanie dokumentów kolumnach: ID, Type, Date, FirstName, LastName, City.
- Filtrowanie:
  - po frazie tekstowej (dla pól: Type, FirstName, LastName, City),
  - po zakresie dat (DateFrom, DateTo).
- Paginacja z możliwością zmiany liczby wierszy na stronę.
- Operacje CRUD:
  - dodania nowego dokumentu,
  - edycji istniejącego dokumentu,
  - usunięcia dokumentu.

### Zarządzanie pozycjami dokumentu

- Po kliknięciu w ikonę rozwinięcia wiersza dokumentu:
  - rozwija się sekcja z listą pozycji dokumentu,
  - każda pozycja zawiera pola: Ordinal, Product, Quantity, Price, TaxRate.
- Operacje CRUD:
  - dodać nową pozycję,
  - edytować istniejącą,
  - usunąć pozycję.

Formularze są dynamiczne, walidowane i automatycznie odświeżają dane po zapisaniu zmian.

### Import danych z plików CSV

#### Aplikacja umożliwia zaimportowanie danych startowych do bazy danych na podstawie dwóch plików CSV:

| Plik              | Opis                    | Nagłówki                                          |
| ----------------- | ----------------------- | ------------------------------------------------- |
| documents.csv     | Dane dokumentów         | Id;Type;Date;FirstName;LastName;City              |
| documentItems.csv | Dane pozycji dokumentów | DocumentId;Ordinal;Product;Quantity;Price;TaxRate |

#### Zasady importu:

- Import jest możliwy tylko wtedy, **gdy baza danych jest pusta**.
- System automatycznie rozpoznaje, który plik jest który, na podstawie struktury nagłówków.
- Jeśli pliki są niezgodne lub dane niepoprawne, proces importu zostanie przerwany z komunikatem błędu.

### Dodatkowe informacje

- Backend i frontend komunikują się przez REST API.
- Dane w tabelach są aktualizowane dynamicznie po każdej operacji.
- Walidacja danych odbywa się zarówno po stronie backendu, jak i frontendu.
- Struktura kodu jest modularna, co ułatwia utrzymanie i rozwój aplikacji.

### Autor

Marcel Wrzeciono

### Licencja

Projekt udostępniony na licencji MIT.
