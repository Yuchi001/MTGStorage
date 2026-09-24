# Bulk import regression tests

Build MTGStorage.csproj (Debug), then compile this standalone test harness using Visual Studio's Roslyn csc.exe:

```powershell
& $csc /nologo /target:exe /out:bin\Debug\BulkImportTests.exe /reference:bin\Debug\MTGStorage.exe /reference:bin\Debug\System.Data.SQLite.dll /reference:System.Data.dll /reference:System.Windows.Forms.dll /reference:System.Drawing.dll Tests\BulkImportTests.cs
& .\bin\Debug\BulkImportTests.exe
```

Set `$csc` to the installed `MSBuild\Current\Bin\Roslyn\csc.exe` path. Tests use an in-memory database and never write to the application's storage. They cover TXT validation, capacity reservations, price tiers, completion gating, excluded rows and sorting, transaction rollback, duplicate quantities, print separation, and card faces.

TXT input uses one card name per line, optionally followed by a positive integer count. Missing counts default to 1. Set codes are not supported:

```text
Sol Ring 2
Lightning Bolt 3
Counterspell
```

Card names are matched using Scryfall fuzzy search (including names containing spaces). Blank lines are ignored. Missing cards, unavailable EUR prices, or insufficient matching storage abort preparation before any cards are saved.
