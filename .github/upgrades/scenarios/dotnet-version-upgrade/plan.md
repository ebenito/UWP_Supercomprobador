# .NET Version Upgrade Plan

## Overview

**Target**: Actualizar 4 proyectos a net10.0 con migración de la app UWP a WinUI 3 / Windows App SDK
**Scope**: Solución pequeña, 4 proyectos (~2 librerías legacy PCL + 1 librería .NET Standard + 1 app UWP con vistas XAML)

### Selected Strategy
**Bottom-Up (Dependency-First)** — Actualizar desde los nodos hoja hacia la aplicación raíz, nivel a nivel.
**Rationale**: 4 proyectos, 2 niveles de dependencia. Los 3 proyectos del nivel 0 son librerías sin SDK-style que deben convertirse antes de actualizar la app UWP del nivel 1.

```
Nivel 1: [SuperComrprobador_UWP] (app UWP → WinUI 3)
			  ↓         ↓         ↓
Nivel 0: [CheckCCC] [IbanLibraryPCL] [SuperComrprobador_UWP.Core]
		 (VB.NET PCL) (C# PCL)         (.NET Standard 2.0)
```

## Tasks

---

### 01-prerequisites: Verificar prerrequisitos y herramientas

Antes de iniciar la actualización, verificar que el SDK de .NET 10 está instalado y que el archivo `global.json` (si existe) es compatible con net10.0. También confirmar que el entorno de desarrollo tiene soporte para Windows App SDK y WinUI 3 (carga de trabajo "Desarrollo de aplicaciones de escritorio de .NET" en Visual Studio).

Este paso garantiza que el resto de tareas se ejecuten sobre una base estable y no fallen por causas del entorno.

**Done when**: .NET 10 SDK instalado y validado. `global.json` (si existe) es compatible. Entorno listo para WinUI 3.

---

### 02-sdk-conversion: Convertir proyectos a formato SDK-style

`CheckCCC` (VB.NET, PCL Profile7) e `IbanLibraryPCL` (C#, PCL Profile7) usan el formato csproj clásico (`<Project ToolsVersion=...>`). `SuperComrprobador_UWP` (UWP) también necesita conversión a SDK-style para poder migrar a WinUI 3. `SuperComrprobador_UWP.Core` ya es SDK-style (`.NET Standard 2.0`) y se excluye.

La conversión debe hacerse en el TFM actual (sin cambiar el framework todavía). Se trata de un cambio estructural del archivo de proyecto, independiente del cambio de TFM, con modos de fallo distintos. Se incluye la migración de `packages.config` a `PackageReference` donde aplique.

Proyectos a convertir: `CheckCCC`, `IbanLibraryPCL`, `SuperComrprobador_UWP`.

**Done when**: Los 3 proyectos tienen formato SDK-style. La solución compila correctamente en los TFMs originales. No se ha cambiado ningún TFM todavía.

---

### 03-foundation-libs: Actualizar librerías base a net10.0

Actualizar los 3 proyectos del Nivel 0 al nuevo TFM: `CheckCCC` (VB.NET), `IbanLibraryPCL` (C#) y `SuperComrprobador_UWP.Core` (.NET Standard 2.0).

- `CheckCCC` e `IbanLibraryPCL`: cambiar TFM de PCL Profile7 a `net10.0`. Ambos son proyectos de lógica de negocio (validación de CCC e IBAN) sin dependencias de paquetes NuGet relevantes para el upgrade.
- `SuperComrprobador_UWP.Core`: cambiar TFM de `netstandard2.0` a `net10.0`. Actualizar `Newtonsoft.Json` de 12.0.3 a 13.0.4 (vulnerabilidad de seguridad incluida). El paquete `NETStandard.Library` puede eliminarse (está implícito en net10.0).

Después de actualizar las 3 librerías, validar que compilan correctamente. El proyecto UWP de nivel superior puede no compilar aún (es esperado en Bottom-Up).

**Done when**: `CheckCCC`, `IbanLibraryPCL` y `SuperComrprobador_UWP.Core` compilan en net10.0. Vulnerabilidad de `Newtonsoft.Json` resuelta (versión 13.0.4). Sin warnings de compilación en estos proyectos.

---

### 04-winui3-migration: Migrar app UWP a WinUI 3 / Windows App SDK

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

---

### 05-final-validation: Validación final de la solución

Validar el estado completo de la solución tras la migración: compilación limpia de todos los proyectos, revisión de dependencias entre proyectos, y documentación de recomendaciones post-migración.

Recomendaciones post-migración a documentar:
- Habilitar Nullable Reference Types en proyectos C# (diferido de esta migración)
- Adoptar Central Package Management (CPM) ahora que todos los proyectos son SDK-style en el mismo ecosistema
- Considerar actualizar `Newtonsoft.Json` → `System.Text.Json` (escenario separado disponible para `SuperComrprobador_UWP.Core`)

**Done when**: La solución completa compila sin errores ni warnings. Todos los proyectos apuntan a net10.0 o net10.0-windows10.0.26100.0. Recomendaciones post-migración documentadas.
