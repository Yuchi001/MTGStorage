; Build through scripts/Build-Installer.ps1. Never package a database.
#ifndef PayloadDir
  #error PayloadDir must point to the isolated release payload
#endif
#ifndef InstallerOutputDir
  #error InstallerOutputDir must be specified
#endif
#define AppVersion GetVersionNumbersString(PayloadDir + "\MTGStorage.exe")

[Setup]
AppId={{841A3A17-639E-411F-9548-EF7D14574528}
AppName=MTGStorage
AppVersion={#AppVersion}
DefaultDirName={localappdata}\Programs\MTGStorage
DisableDirPage=yes
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
OutputDir={#InstallerOutputDir}
OutputBaseFilename=MTGStorage-Setup-{#AppVersion}
SetupIconFile=..\Resources\CardIcon.ico
UninstallDisplayIcon={app}\MTGStorage.exe
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "polish"; MessagesFile: "compiler:Languages\Polish.isl"

[CustomMessages]
english.DotNetTitle=Installing .NET Framework 4.8
polish.DotNetTitle=Instalowanie .NET Framework 4.8
english.DotNetDescription=Downloading the Microsoft installer. Internet access and administrator approval are required.
polish.DotNetDescription=Pobieranie instalatora Microsoft. Wymagany jest internet i zgoda administratora.
english.DotNetReady=Missing .NET Framework 4.8 will be downloaded and installed before MTGStorage. Windows will ask for administrator approval. A restart may be required.
polish.DotNetReady=Brakujący .NET Framework 4.8 zostanie pobrany i zainstalowany przed MTGStorage. Windows poprosi o zgodę administratora. Może być wymagany restart.
english.DotNetDownloadFailed=Could not download or verify .NET Framework 4.8. Check your internet connection and retry. Details: %1
polish.DotNetDownloadFailed=Nie udało się pobrać lub zweryfikować .NET Framework 4.8. Sprawdź połączenie z internetem i ponów próbę. Szczegóły: %1
english.DotNetCancelled=Download cancelled. Retry to install .NET Framework 4.8, or cancel Setup.
polish.DotNetCancelled=Pobieranie anulowane. Ponów próbę instalacji .NET Framework 4.8 lub anuluj instalator.
english.DotNetLaunchFailed=Could not start the .NET installer. Administrator approval is required. Details: %1
polish.DotNetLaunchFailed=Nie udało się uruchomić instalatora .NET. Wymagana jest zgoda administratora. Szczegóły: %1
english.DotNetInstallFailed=The .NET installer did not complete successfully (code %1). Retry, or install .NET Framework 4.8 from https://dotnet.microsoft.com/download/dotnet-framework/net48 and run Setup again.
polish.DotNetInstallFailed=Instalacja .NET nie powiodła się (kod %1). Ponów próbę lub zainstaluj .NET Framework 4.8 ze strony https://dotnet.microsoft.com/download/dotnet-framework/net48 i uruchom instalator ponownie.
english.DotNetNotDetected=.NET Framework 4.8 is still not detected. Restart Windows and run Setup again.
polish.DotNetNotDetected=Nadal nie wykryto .NET Framework 4.8. Uruchom ponownie Windows, a następnie instalator.

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Explicit allowlist: do not replace with a recursive wildcard.
Source: "{#PayloadDir}\MTGStorage.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#PayloadDir}\MTGStorage.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#PayloadDir}\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#PayloadDir}\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#PayloadDir}\x86\SQLite.Interop.dll"; DestDir: "{app}\x86"; Flags: ignoreversion
Source: "{#PayloadDir}\x64\SQLite.Interop.dll"; DestDir: "{app}\x64"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\MTGStorage"; Filename: "{app}\MTGStorage.exe"; WorkingDir: "{app}"
Name: "{autodesktop}\MTGStorage"; Filename: "{app}\MTGStorage.exe"; WorkingDir: "{app}"; Tasks: desktopicon

; No automatic launch and no UninstallDelete: user-created databases survive uninstall.
[Code]
#include "DotNetPrerequisite.iss"
