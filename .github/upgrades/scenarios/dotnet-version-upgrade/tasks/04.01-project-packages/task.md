# 04.01-project-packages: Actualizar proyecto y paquetes NuGet a WinUI 3 / net10.0

## Objetivo
Actualizar el archivo de proyecto SuperComrprobador_UWP.csproj a net10.0-windows10.0.26100.0 y reemplazar todos los paquetes UWP legacy.

## Alcance
- Cambiar TFM de net6.0-windows10.0.26100.0 a net10.0-windows10.0.26100.0
- Reemplazar Microsoft.NETCore.UniversalWindowsPlatform 6.2.12 por:
  - Microsoft.WindowsAppSDK 2.1.3
  - Microsoft.Graphics.Win2D 1.1.0
  - Microsoft.Windows.Compatibility 10.0.8
- Verificar que Microsoft.Xaml.Behaviors.WinUI.Managed 2.0.9 ya está en el proyecto (añadido en tarea 02)
- Eliminar propiedades obsoletas de UWP (WindowsXamlEnableOverview, etc.) si las hay
- Validar que el proyecto restaura correctamente los nuevos paquetes

**Done when**: csproj apunta a net10.0-windows10.0.26100.0, paquetes UWP legacy eliminados, WindowsAppSDK 2.1.3 referenciado, dotnet restore sin errores.
