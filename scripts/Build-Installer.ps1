[CmdletBinding()]
param(
    [switch]$BuildOnly,
    [string]$MSBuildPath,
    [string]$IsccPath
)
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
if (-not $MSBuildPath) {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    if (Test-Path -LiteralPath $vswhere) {
        $MSBuildPath = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
    }
}
if (-not $MSBuildPath -or -not (Test-Path -LiteralPath $MSBuildPath)) {
    throw 'Install Visual Studio Build Tools with .NET desktop build tools and the .NET Framework 4.8 targeting pack, or pass -MSBuildPath.'
}
if (-not $BuildOnly) {
    if (-not $IsccPath) {
        $candidates = @(
            (Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6\ISCC.exe'),
            (Join-Path $env:ProgramFiles 'Inno Setup 6\ISCC.exe')
        )
        $IsccPath = $candidates | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1
    }
    if (-not $IsccPath -or -not (Test-Path -LiteralPath $IsccPath)) {
        throw 'Install Inno Setup 6 or pass -IsccPath. Use -BuildOnly to verify the release payload without Inno Setup.'
    }
}
# A fresh directory on every invocation keeps Debug, Release and all existing databases untouched.
$buildRoot = Join-Path $projectRoot ('artifacts\installer\' + [Guid]::NewGuid().ToString('N'))
$payload = Join-Path $buildRoot 'payload'
$intermediate = Join-Path $buildRoot 'obj'
New-Item -ItemType Directory -Path $payload, $intermediate -Force | Out-Null
& $MSBuildPath (Join-Path $projectRoot 'MTGStorage.csproj') /nologo /t:Build /p:Configuration=Release /p:Platform=AnyCPU "/p:OutputPath=$payload\" "/p:BaseIntermediateOutputPath=$intermediate\" "/p:IntermediateOutputPath=$intermediate\"
if ($LASTEXITCODE -ne 0) { throw 'Release build failed.' }
$required = @('MTGStorage.exe', 'MTGStorage.exe.config', 'Newtonsoft.Json.dll', 'System.Data.SQLite.dll', 'x86\SQLite.Interop.dll', 'x64\SQLite.Interop.dll')
foreach ($file in $required) {
    if (-not (Test-Path -LiteralPath (Join-Path $payload $file))) { throw "Missing installer dependency: $file" }
}
if (Get-ChildItem -LiteralPath $payload -Recurse -File | Where-Object { $_.Name -match '\.(db|sqlite)(-|$)' }) {
    throw 'A database was found in the release payload. Packaging stopped.'
}
Write-Host "Verified release payload: $payload"
if (-not $BuildOnly) {
    $output = Join-Path $buildRoot 'setup'
    & $IsccPath "/DPayloadDir=$payload" "/DInstallerOutputDir=$output" (Join-Path $projectRoot 'installer\MTGStorage.iss')
    if ($LASTEXITCODE -ne 0) { throw 'Installer compilation failed.' }
    Write-Host "Installer output: $output"
}
