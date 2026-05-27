# Script para lanzar el emulador sin abrir Android Studio
$emulatorPath = "C:\Users\Nicolas\AppData\Local\Android\Sdk\emulator\emulator.exe"
$avdName = "Medium_Phone"

Write-Host "Iniciando emulador: $avdName..." -ForegroundColor Cyan

Start-Process $emulatorPath -ArgumentList "-avd", $avdName, "-no-snapshot-load"
