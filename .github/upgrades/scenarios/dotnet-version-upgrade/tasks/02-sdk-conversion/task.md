# 02-sdk-conversion: Convertir proyectos a formato SDK-style

`CheckCCC` (VB.NET, PCL Profile7) e `IbanLibraryPCL` (C#, PCL Profile7) usan el formato csproj clásico (`<Project ToolsVersion=...>`). `SuperComrprobador_UWP` (UWP) también necesita conversión a SDK-style para poder migrar a WinUI 3. `SuperComrprobador_UWP.Core` ya es SDK-style (`.NET Standard 2.0`) y se excluye.

La conversión debe hacerse en el TFM actual (sin cambiar el framework todavía). Se trata de un cambio estructural del archivo de proyecto, independiente del cambio de TFM, con modos de fallo distintos. Se incluye la migración de `packages.config` a `PackageReference` donde aplique.

Proyectos a convertir: `CheckCCC`, `IbanLibraryPCL`, `SuperComrprobador_UWP`.

**Done when**: Los 3 proyectos tienen formato SDK-style. La solución compila correctamente en los TFMs originales. No se ha cambiado ningún TFM todavía.
