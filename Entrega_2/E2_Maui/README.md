# App de Propinas (MAUI)

Aplicacion movil para calcular consumo y propina en restaurante con division por persona. Implementa MVVM con CommunityToolkit.Mvvm.

## Clonar el repositorio

```bash
git clone https://github.com/RazorLazor27/AppPropinaMAUI.git
```

## Ejecutar

1. Abre la carpeta del proyecto en VS Code.
2. Asegura que el SDK de .NET MAUI este instalado.
3. Ejecuta el proyecto en el dispositivo o emulador de tu preferencia.

## Comando para ejecutar

1. dotnet clean (Para botar todo la basura generada de la ultima compilación)
2. dotnet build -t:Run -f net10.0-android

## Guia para Modificar el Diseno

Esta guia esta pensada para cambiar la apariencia sin tocar la logica ni los bindings.

- Colores y estilos globales: Resources/Styles/Colors.xaml y Resources/Styles/Styles.xaml.
- Estilos principales del layout (tipografia, padding, bordes, tamanos): Resources/Styles/Styles.xaml.
- Tarjeta de resultados: en MainPage.xaml, el contenedor con x:Name="ResultsCard".
- Botones de propina: en MainPage.xaml, el Grid con x:Name="TipButtonsPanel".
- Slider de propina: en MainPage.xaml, el Slider con x:Name="TipSlider".

Advertencia: No borres ni modifiques los atributos con {Binding ...} ni los comandos. Esos bindings conectan la vista con el ViewModel y si se eliminan la app deja de calcular.

## Estructura MVVM

- Modelo: Models/TipCalculator.cs
- ViewModel: ViewModels/MainViewModel.cs
- Vista: MainPage.xaml y MainPage.xaml.cs
