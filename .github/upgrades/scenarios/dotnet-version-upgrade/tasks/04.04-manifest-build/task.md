# 04.04-manifest-build: Actualizar manifiesto MSIX y validar compilación

## Objetivo
Actualizar Package.appxmanifest para que sea compatible con WinUI 3 / Windows App SDK, y validar que el proyecto compila completamente.

## Alcance
- Actualizar Package.appxmanifest: cambiar el EntryPoint a $targetentrypoint (convención Windows App SDK)
- Verificar que MinVersion y MaxVersionTested son compatibles con net10.0-windows10.0.26100.0
- Eliminar capacidades o extensiones específicas de UWP que no aplican en WinUI 3
- Compilar el proyecto completo y resolver cualquier error de compilación restante

**Done when**: Package.appxmanifest actualizado para Windows App SDK. Proyecto compila sin errores. Sin warnings en proyectos modificados.
