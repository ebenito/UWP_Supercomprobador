# 04.03-codebehind-migration: Migrar code-behind y servicios de Windows.UI a Microsoft.UI

## Objetivo
Actualizar todos los archivos .cs del proyecto que usan tipos de Windows.UI.Xaml a sus equivalentes en Microsoft.UI.Xaml (WinUI 3).

## Archivos afectados (revisar todos los .cs)
- App.xaml.cs — modelo de activación WinUI 3
- Activation/ActivationHandler.cs, DefaultActivationHandler.cs
- Services/ActivationService.cs, NavigationService.cs, ThemeSelectorService.cs
- Helpers/EnumToBooleanConverter.cs, Observable.cs, RelayCommand.cs, ResourceExtensions.cs
- Helpers/SettingsStorageExtensions.cs — adaptar AsTask() (Api.0002)
- ViewModels/*.cs — revisar referencias
- Views/*.xaml.cs — actualizar using statements
- BotonListTemplateSelector.cs

## Cambios principales
- using Windows.UI.Xaml → using Microsoft.UI.Xaml
- using Windows.UI.Xaml.Controls → using Microsoft.UI.Xaml.Controls
- using Windows.UI → using Microsoft.UI (colores, etc.)
- Adaptar SettingsStorageExtensions.cs: AsTask() de WindowsRuntimeSystemExtensions pasa a ser .AsTask() directamente (WinRT en .NET 10 lo soporta nativamente)
- Revisar IActivationHandler si usa tipos específicos de UWP

**Done when**: Todos los archivos .cs usan namespaces Microsoft.UI o WinRT nativos. SettingsStorageExtensions.cs compilando correctamente. Sin using Windows.UI.Xaml en código fuente.
