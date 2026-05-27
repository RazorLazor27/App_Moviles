# Script para lanzar el emulador sin abrir Android Studio
$emulatorPath = "C:\Users\Nicolas\AppData\Local\Android\Sdk\emulator\emulator.exe"
$avdName = "Medium_Phone"

Write-Host "Iniciando emulador: $avdName..." -ForegroundColor Cyan

# El '&' es para ejecutar el comando y '-detachable' permite que el emulador 
# siga vivo aunque cierres la terminal (dependiendo de la versión del SDK)
Start-Process $emulatorPath -ArgumentList "-avd", $avdName, "-no-snapshot-load"
