# 03-foundation-libs: Actualizar librerías base a net10.0

Actualizar los 3 proyectos del Nivel 0 al nuevo TFM: `CheckCCC` (VB.NET), `IbanLibraryPCL` (C#) y `SuperComrprobador_UWP.Core` (.NET Standard 2.0).

- `CheckCCC` e `IbanLibraryPCL`: cambiar TFM de PCL Profile7 a `net10.0`. Ambos son proyectos de lógica de negocio (validación de CCC e IBAN) sin dependencias de paquetes NuGet relevantes para el upgrade.
- `SuperComrprobador_UWP.Core`: cambiar TFM de `netstandard2.0` a `net10.0`. Actualizar `Newtonsoft.Json` de 12.0.3 a 13.0.4 (vulnerabilidad de seguridad incluida). El paquete `NETStandard.Library` puede eliminarse (está implícito en net10.0).

Después de actualizar las 3 librerías, validar que compilan correctamente. El proyecto UWP de nivel superior puede no compilar aún (es esperado en Bottom-Up).

**Done when**: `CheckCCC`, `IbanLibraryPCL` y `SuperComrprobador_UWP.Core` compilan en net10.0. Vulnerabilidad de `Newtonsoft.Json` resuelta (versión 13.0.4). Sin warnings de compilación en estos proyectos.
