# Budowanie instalatora MTGStorage

Projekt pozostaje aplikacją WinForms na .NET Framework 4.8. Kod obsługi bazy i jej ścieżka nie zostały zmienione.

## Wymagania

- Visual Studio 2022 lub Build Tools z narzędziami kompilacji aplikacji desktopowych .NET i targeting pack .NET Framework 4.8.
- Pakiety z `packages.config` odtworzone do katalogu `packages` (na nowym komputerze: `nuget restore MTGStorage.sln`).
- [Inno Setup 6](https://jrsoftware.org/isdl.php) do utworzenia instalatora EXE.

## Komendy (PowerShell w katalogu projektu)

```powershell
# Sama kompilacja Release i sprawdzenie obecności zależności:
.\scripts\Build-Installer.ps1 -BuildOnly

# Kompilacja aplikacji i instalatora:
.\scripts\Build-Installer.ps1

# Niestandardowa lokalizacja kompilatora instalatora:
.\scripts\Build-Installer.ps1 -IsccPath 'C:\Tools\Inno Setup 6\ISCC.exe'
```

Każde wywołanie tworzy nowy katalog `artifacts\installer\<identyfikator>`. Pliki aplikacji trafiają do `payload`, a instalator do `setup`. Skrypt nie czyści ani nie nadpisuje `bin\Debug`, `bin\Release` ani istniejących baz. Nie uruchamia aplikacji ani instalatora. Wersja instalatora pochodzi z `AssemblyFileVersion` w `Properties\AssemblyInfo.cs`; zwiększ ją przed kolejnym wydaniem. Zachowaj stałe `AppId`, aby aktualizować tę samą instalację.

## Dane użytkownika

Instalacja jest przeznaczona dla bieżącego konta, bez uprawnień administratora, w `%LOCALAPPDATA%\Programs\MTGStorage`. Aplikacja nadal zapisuje `mtgstorage.db` obok swojego EXE. Nowa instalacja otrzyma pustą bazę dopiero po pierwszym uruchomieniu. Nie pobiera danych z wersji deweloperskiej.

Instalator zawiera jawną listę sześciu plików aplikacji i bibliotek, bez baz, testów czy plików z `bin\Debug`. Aktualizacja nadpisuje tylko pliki programu. Deinstalator nie usuwa bazy utworzonej przez aplikację. Nie dodawaj reguł `UninstallDelete` usuwających katalog aplikacji. Zachowanie to opiera się na [zasadach Inno Setup](https://jrsoftware.org/ishelp/topic_uninstalldeletesection.htm).

Obecna baza `bin\Debug\mtgstorage.db` pozostaje w swojej lokalizacji. Jeśli zechcesz przenieść kolekcję do zainstalowanej wersji, użyj eksportu ZIP w dotychczasowej aplikacji i importu w nowej. Import zastępuje zawartość docelowej bazy; zachowaj eksport oraz oryginalną bazę. Ten proces nie jest wykonywany przez skrypt ani instalator.

## Weryfikacja wydania

Na osobnym koncie lub maszynie testowej sprawdź: instalację, pierwsze uruchomienie i utworzenie kolekcji, aktualizację z zachowaniem kolekcji, deinstalację i ponowną instalację z zachowaniem bazy.

### Automatyczna instalacja .NET Framework

Jeżeli .NET Framework 4.8 lub nowszy zgodny framework jest już obecny, instalator pomija pobieranie i nie prosi o administratora. W przeciwnym razie po kliknięciu Instaluj pobiera oficjalny instalator internetowy Microsoft przez HTTPS, sprawdza przypiętą sumę SHA-256 i uruchamia go z podniesieniem uprawnień. Potrzebne są internet oraz zgoda administratora. Tylko instalacja frameworka jest podnoszona do administratora, więc katalog MTGStorage nadal należy do pierwotnego użytkownika.

Po powodzeniu instalacja aplikacji jest kontynuowana. Kody .NET 3010 i 1641 oznaczają potrzebę restartu: kreator proponuje restart na końcu, nie uruchamia automatycznie aplikacji. Błędy pobierania, anulowanie UAC oraz błędy instalacji .NET zatrzymują instalację MTGStorage z komunikatem i możliwością ponowienia próby. Parametr `/norestart` zapobiega samodzielnemu restartowi przez instalator .NET; przy automatycznym wdrożeniu podaj też `/NORESTART` instalatorowi MTGStorage.

Sprawdź na maszynie wirtualnej bez .NET 4.8: udaną instalację, brak internetu, anulowanie pobierania, odmowę UAC i restart. Nie usuwaj .NET z używanego komputera do testów. Sama kompilacja nie weryfikuje tych scenariuszy systemowych.

Źródło pliku: [Microsoft .NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48). Parametry i kody zakończenia: [instrukcja wdrażania Microsoft](https://learn.microsoft.com/en-us/dotnet/framework/deployment/deployment-guide-for-developers). URL i SHA-256 są zapisane w `DotNetPrerequisite.iss`. Przed ich zmianą pobierz nowy plik, sprawdź poprawność podpisu Microsoft przez `Get-AuthenticodeSignature` i policz `Get-FileHash -Algorithm SHA256`. Nie wyłączaj sprawdzania sumy kontrolnej.

Instalator nie jest podpisany cyfrowo. Konfiguracja nie zawiera certyfikatu ani automatycznego publikowania. Dotychczasowe wersje zależności są zachowane, w tym Newtonsoft.Json 13.0.5-beta1.
