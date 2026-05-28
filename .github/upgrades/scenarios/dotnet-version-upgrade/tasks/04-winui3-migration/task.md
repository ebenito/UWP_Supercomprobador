# 04-winui3-migration: Migrar app UWP a WinUI 3 / Windows App SDK

Migrar `SuperComrprobador_UWP` de UWP (`.NETCore,Version=v5.0`) a WinUI 3 con Windows App SDK, apuntando a `net10.0-windows10.0.26100.0`.

Esta es la tarea más compleja del upgrade. Implica:

- Cambiar el TFM del proyecto a `net10.0-windows10.0.26100.0` y el tipo de proyecto a una app Win32 empaquetada con MSIX
- Reemplazar `Microsoft.NETCore.UniversalWindowsPlatform 6.2.12` por `Microsoft.WindowsAppSDK 2.1.3`, `Microsoft.Graphics.Win2D 1.1.0` y `Microsoft.Windows.Compatibility 10.0.8`
- Reemplazar `Microsoft.Xaml.Behaviors.Uwp.Managed 2.0.1` por `Microsoft.Xaml.Behaviors.WinUI.Managed` (equivalente WinUI 3)
- Actualizar `App.xaml` / `App.xaml.cs` al modelo de activación de Windows App SDK (`Microsoft.UI.Xaml.Application`)
- Migrar las vistas XAML (MainPage, P1NIFPage, P2CCCPage, P3IbanPage, SettingsPage) de `Windows.UI.Xaml` a `Microsoft.UI.Xaml`
- Adaptar `SettingsStorageExtensions.cs` — el uso de `System.WindowsRuntimeSystemExtensions.AsTask()` (Api.0002) requiere ajuste: en WinUI 3 / net10.0 las APIs de WinRT son accesibles directamente sin extensiones de WinRT del namespace `System`
- El paquete `Microsoft.Services.Store.Engagement` es compatible; verificar que sigue funcionando en WinUI 3

El proyecto necesita un manifiesto MSIX (`Package.appxmanifest`) actualizado para la identidad de la app Win32 empaquetada.

**Done when**: `SuperComrprobador_UWP` compila y ejecuta en net10.0-windows10.0.26100.0 como app WinUI 3. Todas las vistas XAML se cargan. Paquetes UWP legacy eliminados. Sin warnings de compilación.
