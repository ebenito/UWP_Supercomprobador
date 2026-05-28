# Progress Details — 04.03-codebehind-migration

## What was done

Migración de todos los archivos .cs del proyecto de Windows.UI.Xaml a Microsoft.UI.Xaml (WinUI 3).

### Archivos modificados

| Archivo | Cambios |
|---|---|
| `App.xaml.cs` | using Windows.ApplicationModel.Activation → Microsoft.UI.Xaml; OnLaunched simplificado para WinUI 3 |
| `Services/ActivationService.cs` | using Windows.UI.Xaml.* → Microsoft.UI.Xaml.*; añadido Windows.System para VirtualKey; IsInteractive usa LaunchActivatedEventArgs |
| `Services/NavigationService.cs` | using Windows.UI.Xaml.* → Microsoft.UI.Xaml.* |
| `Services/ThemeSelectorService.cs` | Reescrito SetRequestedThemeAsync: eliminado CoreApplication.Views/CoreDispatcher (UWP), reemplazado por DispatcherQueue de WinUI 3 |
| `Helpers/EnumToBooleanConverter.cs` | using Windows.UI.Xaml.Data → Microsoft.UI.Xaml.Data |
| `Helpers/SettingsStorageExtensions.cs` | Añadido using System.Runtime.InteropServices.WindowsRuntime para AsTask() |
| `BotonListTemplateSelector.cs` | Reescrito: eliminado Windows.UI.ViewManagement.ApplicationView (UWP sin equivalente en Win32); simplificado para usar VisualTreeHelper de Microsoft.UI.Xaml.Media |
| `ViewModels/SettingsViewModel.cs` | using Windows.UI.Xaml → Microsoft.UI.Xaml; StoreServicesFeedbackLauncher marcado como TODO (no compatible con WinUI 3 desktop) |
| `Views/MainPage.xaml.cs` | using Windows.UI.Xaml.Controls → Microsoft.UI.Xaml.Controls |
| `Views/P1NIFPage.xaml.cs` | Ídem |
| `Views/P2CCCPage.xaml.cs` | Ídem |
| `Views/P3IbanPage.xaml.cs` | Ídem |
| `Views/SettingsPage.xaml.cs` | using Windows.UI.Xaml.* → Microsoft.UI.Xaml.* |
| `SuperComrprobador_UWP.csproj` | Añadidos: `Platforms`, `SupportedOSPlatformVersion`, `NoWarn CA1416/NETSDK1206` |

## Build results

✅ Compilación correcta. 0 errores.
1 warning de Win2D (AnyCPU platform) — comportamiento esperado; desaparece al compilar con plataforma específica (x64/x86).

## Issues resueltos

- `Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher` no es compatible con WinUI 3 desktop — deshabilitado con TODO
- `CoreApplication.Views` / `CoreDispatcher` (UWP) → `DispatcherQueue` de WinUI 3
- `Windows.UI.ViewManagement.ApplicationView` (UWP) → eliminado, lógica de orientación no aplica en apps de escritorio Win32
