# 04.02-xaml-migration: Migrar XAML de Windows.UI.Xaml a Microsoft.UI.Xaml

## Objetivo
Actualizar todos los archivos XAML del proyecto de los namespaces UWP (Windows.UI.Xaml) a los namespaces WinUI 3 (Microsoft.UI.Xaml).

## Archivos afectados
- App.xaml
- Views/MainPage.xaml
- Views/P1NIFPage.xaml
- Views/P2CCCPage.xaml
- Views/P3IbanPage.xaml
- Views/SettingsPage.xaml
- Styles/Page.xaml, TextBlock.xaml, _Colors.xaml, _FontSizes.xaml, _Thickness.xaml

## Cambios principales
- xmlns:x → mantener igual
- Cambiar xmlns por defecto o atributos de clase WinUI 3
- Actualizar referencias a tipos de Windows.UI a Microsoft.UI en XAML
- Actualizar App.xaml para usar Microsoft.UI.Xaml.Application

**Done when**: Todos los archivos XAML usan namespaces Microsoft.UI.Xaml. Sin referencias a Windows.UI.Xaml en XAML.
