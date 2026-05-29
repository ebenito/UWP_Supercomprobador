# SuperComprobador

Aplicación Windows para verificar la validez de documentos de identificación y bancarios españoles: **NIF/NIE**, **CCC** (Código Cuenta Cliente) e **IBAN**.

## Funcionalidades

| Módulo | Descripción |
|---|---|
| **Comprueba NIF** | Valida el formato y la letra de control de un NIF (DNI) o NIE |
| **Comprueba CCC** | Valida los 4 campos del Código Cuenta Cliente (Banco, Oficina, DC y Cuenta) comprobando los dígitos de control |
| **Comprueba IBAN** | Valida un IBAN español comprobando el checksum según la norma ISO 13616 |

## Arquitectura

El proyecto sigue el patrón **MVVM** con separación en capas:

```
SuperComprobador (solución)
├── SuperComrprobador_UWP        → App principal WinUI 3 (.NET 10)
│   ├── Views/                   → Páginas XAML (MainPage, P1NIF, P2CCC, P3Iban, Settings)
│   ├── ViewModels/              → Lógica de presentación (Observable + RelayCommand)
│   ├── Services/                → ActivationService, NavigationService, ThemeSelectorService
│   ├── Models/                  → Modelos de datos del menú
│   ├── Helpers/                 → Converters y utilidades
│   └── Styles/                  → Diccionarios de recursos XAML
├── SuperComrprobador_UWP.Core   → Lógica de negocio compartida (.NET Standard)
├── CheckCCC                     → Librería de validación CCC (VB.NET)
└── IbanLibraryPCL               → Librería de validación IBAN (PCL)
```

## Requisitos

- Windows 10 versión 1903 (build 18362) o superior
- Visual Studio 2022 / 2026 con la carga de trabajo **Windows App SDK**
- SDK de Windows 10.0.26100.0

## Compilación y ejecución

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/ebenito/UWP_Supercomprobador.git
   ```
2. Abrir `UWP_Supercomprobador.sln` en Visual Studio.
3. Seleccionar plataforma **x64** o **ARM64**.
4. Compilar y ejecutar (`F5`).

---

## Migración de UWP a WinUI 3 con Windows App SDK

### Contexto

El proyecto nació como una aplicación **UWP** (Universal Windows Platform) orientada a .NET Native / .NET 5. Con el objetivo de modernizarlo a **.NET 10** y alinearlo con el futuro de las aplicaciones Windows de escritorio, se migró a **WinUI 3** usando **Windows App SDK**.

### ¿Por qué WinUI 3?

| Aspecto | UWP | WinUI 3 + Windows App SDK |
|---|---|---|
| Framework | .NET Native / .NET 5 UWP | .NET 10 |
| UI | `Windows.UI.Xaml` | `Microsoft.UI.Xaml` |
| Distribución | Solo Microsoft Store | Store + MSIX + instalador clásico |
| Ciclo de vida de Windows | Ligado al OS | Actualizable de forma independiente |
| Soporte futuro | En mantenimiento | Inversión activa de Microsoft |

### Cambios realizados en la migración

#### 1. Cambio del TFM (Target Framework Moniker)

```xml
<!-- Antes (UWP) -->
<TargetFramework>uap10.0.19041</TargetFramework>

<!-- Después (WinUI 3 / .NET 10) -->
<TargetFramework>net10.0-windows10.0.26100.0</TargetFramework>
<UseWinUI>true</UseWinUI>
```

#### 2. Sustitución de NuGet packages

| Package eliminado (UWP) | Package añadido (WinUI 3) |
|---|---|
| `Microsoft.UI.Xaml` (WinUI 2) | `Microsoft.WindowsAppSDK` 2.1.3 |
| — | `Microsoft.Xaml.Behaviors.WinUI.Managed` 3.0.1 |
| — | `Microsoft.Graphics.Win2D` 1.4.0 |

#### 3. Namespace de `using` en todo el código C#

```csharp
// Antes
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Después
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
```

#### 4. Nuevo modelo de ventana (`Window`)

En WinUI 3 ya no existe el concepto de ventana implícita de UWP. Se crea y activa la ventana explícitamente en `App.xaml.cs`:

```csharp
protected override async void OnLaunched(LaunchActivatedEventArgs args)
{
	MainWindow = new Window();
	MainWindow.Activate();
	await ActivationService.ActivateAsync(args);
}
```

#### 5. `App.xaml` — carga de recursos WinUI 3

Se añadió `XamlControlsResources` como primer diccionario en `MergedDictionaries`. Esto es **obligatorio** en WinUI 3: sin él los recursos de tema (`CardBackgroundFillColorDefaultBrush`, etc.) no están disponibles en tiempo de ejecución y se produce una `XamlParseException`.

```xml
<Application.Resources>
	<ResourceDictionary>
		<ResourceDictionary.MergedDictionaries>
			<!-- Recursos base de WinUI 3: DEBE ser el primero -->
			<controls:XamlControlsResources />
			<!-- ... resto de diccionarios de la app -->
		</ResourceDictionary.MergedDictionaries>
	</ResourceDictionary>
</Application.Resources>
```

#### 6. Plataformas de destino

UWP soportaba x86/x64/ARM. WinUI 3 empaquetado solo admite **x64** y **ARM64**:

```xml
<Platforms>x64;ARM64</Platforms>
```

#### 7. Ajuste de `OnLaunched`

La firma del método cambió ligeramente:

```csharp
// Antes (UWP)
protected override void OnLaunched(LaunchActivatedEventArgs e) { }

// Después (WinUI 3)
protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) { }
```

### Estado actual

La migración está en la rama `dotnet-version-upgrade`. El proyecto compila y ejecuta sobre **.NET 10** con **Windows App SDK 2.1.3** y **WinUI 3**.

### Referencias

- [Migración de UWP a WinUI 3 — Microsoft Learn](https://learn.microsoft.com/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Windows App SDK](https://learn.microsoft.com/windows/apps/windows-app-sdk/)
- [Novedades de WinUI 3](https://learn.microsoft.com/windows/apps/winui/winui3/)
