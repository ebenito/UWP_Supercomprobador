# Upgrade Options — SuperComprobador_PRO

Assessment: 4 proyectos (2 librerías PCL legacy + 1 librería .NET Standard 2.0 + 1 app UWP), migrando a net10.0. 21 problemas detectados (7 obligatorios). El usuario ha solicitado migrar la app UWP a WinUI 3 con Windows App SDK.

## Strategy

### Upgrade Strategy
Solución con 4 proyectos en 2 niveles de dependencia. Los proyectos `CheckCCC` e `IbanLibraryPCL` son librerías PCL en formato csproj clásico (no SDK-style), y `SuperComrprobador_UWP` depende de los tres proyectos de nivel 0. Se cruza la frontera PCL/.NET Framework → modern .NET, lo que hace Bottom-Up fijo (no configurable).

| Value | Description |
|-------|-------------|
| **Bottom-Up** (selected) | Actualiza primero las librerías de nivel 0, luego la app de nivel 1. Cada nivel se valida independientemente. Fijo para soluciones con proyectos no SDK-style cruzando a .NET moderno. |

## Project Structure

### Package Management
Solución con 3 proyectos no SDK-style y cruce de frontera PCL → .NET moderno. Añadir CPM durante la migración crearía fricción innecesaria con `VersionOverride`.

| Value | Description |
|-------|-------------|
| **Per-Project (defer CPM to post-migration)** (selected) | Cada proyecto mantiene sus propias versiones durante la migración. CPM se recomienda como paso post-migración cuando todos los proyectos estén en SDK-style y en el mismo ecosistema. |
| Central Package Management (CPM) | Crea `Directory.Packages.props` y centraliza versiones. No recomendado durante migraciones activas de Framework. |

## Compatibility

### Unsupported Packages
`Microsoft.Xaml.Behaviors.Uwp.Managed 2.0.1` no tiene versión compatible para net10.0 (1 paquete). Durante la migración a WinUI 3, deberá reemplazarse por `Microsoft.Xaml.Behaviors.WinUI.Managed`.

| Value | Description |
|-------|-------------|
| **Resolve Inline** (selected) | Investiga y resuelve el paquete incompatible dentro de la misma tarea. Reemplaza por el equivalente de WinUI 3. |
| Defer Resolution | Genera stubs mínimos y crea tareas de seguimiento. Apropiado cuando hay más de 3 paquetes sin reemplazo conocido. |

### Unsupported API Handling
`System.WindowsRuntimeSystemExtensions.AsTask()` detectado en `SettingsStorageExtensions.cs` (Api.0002). Esta API de WinRT necesita adaptación durante la migración a WinUI 3 / net10.0-windows.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resuelve todos los cambios de API en la misma tarea, incluidos los complejos. Sin trabajo diferido. |
| Defer Complex Changes | Aplica cambios simples inmediatamente y genera stubs para los complejos. |

## Modernization

### Nullable Reference Types
La solución tiene 4 proyectos (≤5) pero incluye VB.NET (`CheckCCC`) y la migración ya es de alta complejidad (UWP → WinUI 3 + conversión SDK-style de 3 proyectos). Conviene habilitar NRTs como paso separado tras estabilizar la migración.

| Value | Description |
|-------|-------------|
| **Leave Disabled** (selected) | No habilita nullable durante esta migración. Mantiene el comportamiento actual de null. Se recomienda habilitarlo por separado tras completar la migración. |
| Enable Nullable Reference Types | Añade `<Nullable>enable</Nullable>` a los proyectos C#. Seguridad en tiempo de compilación, puede requerir actualizaciones de código. |
