# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (LTS)

## Source Control
- **Source Branch**: master
- **Working Branch**: dotnet-version-upgrade
- **Commit Strategy**: After Each Task

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: Bottom-Up (fija — solución con proyectos no SDK-style cruzando a .NET moderno)

### Project Structure
- Package Management: Per-Project (defer CPM to post-migration)

### Compatibility
- Unsupported Packages: Resolve Inline (1 paquete incompatible: Microsoft.Xaml.Behaviors.Uwp.Managed → Microsoft.Xaml.Behaviors.WinUI.Managed)
- Unsupported API Handling: Fix Inline

### Modernization
- Nullable Reference Types: Leave Disabled (habilitar por separado tras la migración)

## Strategy
**Selected**: Bottom-Up (Dependency-First)
**Rationale**: 4 proyectos, 2 niveles de dependencia. Proyectos PCL legacy no SDK-style cruzando a .NET 10. Migración UWP → WinUI 3 en nivel 1.

### Execution Constraints
- Estricto orden de niveles: el Nivel 0 debe completar y validar antes de iniciar el Nivel 1
- La conversión SDK-style es una tarea separada del cambio de TFM — no fusionar
- Tras completar cada nivel, verificar que los proyectos de nivel superior siguen compilando
- La migración WinUI 3 incluye reemplazo de paquetes UWP legacy, actualización de XAML y adaptación de APIs WinRT
- Validación entre niveles: tras el Nivel 0, confirmar que SuperComrprobador_UWP puede resolver sus referencias antes de proceder
