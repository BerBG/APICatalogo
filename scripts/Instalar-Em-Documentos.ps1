# Clona o APICatalogo em Documentos e abre no Visual Studio 2022.
# Execute no PowerShell: irm <url raw> | iex   OU   .\scripts\Instalar-Em-Documentos.ps1

$ErrorActionPreference = "Stop"
$repoUrl = "https://github.com/BerBG/APICatalogo.git"
$dest = Join-Path $env:USERPROFILE "Documents\APICatalogo"
$sln = Join-Path $dest "APICatalogo.sln"

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Error "Git não encontrado. Instale o Git for Windows: https://git-scm.com/download/win"
}

if (Test-Path $dest) {
    Write-Host "Pasta já existe: $dest"
    Write-Host "Atualizando com git pull..."
    Push-Location $dest
    git pull
    Pop-Location
} else {
    Write-Host "Clonando em $dest ..."
    git clone $repoUrl $dest
}

$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$devenv = $null
if (Test-Path $vswhere) {
    $devenv = & $vswhere -latest -products * -requires Microsoft.ComponentGroup.CoreEditor -property installationPath
    if ($devenv) {
        $devenv = Join-Path $devenv "Common7\IDE\devenv.exe"
    }
}

if (-not $devenv -or -not (Test-Path $devenv)) {
    Write-Warning "Visual Studio 2022 não encontrado. Abra manualmente: $sln"
    explorer.exe $dest
    exit 0
}

Write-Host "Abrindo Visual Studio 2022..."
Start-Process $devenv -ArgumentList "`"$sln`""
