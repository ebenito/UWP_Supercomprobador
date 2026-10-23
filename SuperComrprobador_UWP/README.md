# Súper Comprobador

Aplicación de escritorio para Windows que comprueba la validez de documentos de identificación y bancarios españoles. Valida NIF y NIE, el Código Cuenta Cliente (CCC) y el IBAN.

[![Microsoft Store](https://img.shields.io/badge/Microsoft%20Store-S%C3%BAper%20Comprobador-0078D4?logo=windows&logoColor=white)](https://apps.microsoft.com/detail/9nblggh08mfc?hl=es-ES&gl=ES)
![WinUI 3](https://img.shields.io/badge/WinUI-3-blue)
![.NET 10](https://img.shields.io/badge/.NET-10-512BD4)
![Windows App SDK](https://img.shields.io/badge/Windows%20App%20SDK-2.5-0078D4)

Es un proyecto personal con fines de aprendizaje. Cada versión ha servido para probar la plataforma Windows del momento, desde Windows Phone hasta WinUI 3.

## Historia del proyecto

**2012. Windows Phone.** Súper Comprobador nació en junio de 2012 como app para Windows Phone, publicada bajo el nombre de editor AppsWP y pensada para comerciales que necesitaban validar documentos a pie de cliente. Aquella primera versión tenía cuatro herramientas en lugar de tres, porque además de NIF, CCC e IBAN incluía una calculadora de descuentos. También sabía recomponer un NIF o un CCC con un dígito erróneo, y se distribuía en dos variantes, una de prueba con publicidad y otra PRO con todas las funciones. La ficha original sigue en la tienda: [Súper Comprobador en Microsoft Store](https://apps.microsoft.com/detail/9nblggh08mfc?hl=es-ES&gl=ES).

**Windows 8.1.** Con la llegada de las apps de tienda para escritorio, la app se reescribió en XAML para Windows 8.1 con Visual Studio 2013. Ese código se conserva en la carpeta `SuperComprobador` como referencia histórica y ya no forma parte de la solución.

**2021. UWP.** En abril de 2021 se migró a Universal Windows Platform para Windows 10 usando Windows Template Studio, que aportó la estructura MVVM, los servicios de navegación y la página de ajustes con selector de tema. Es el origen del proyecto `SuperComrprobador_UWP`, errata incluida.

**2026. WinUI 3 y .NET 10.** Entre mayo y junio de 2026 la app dio el salto a WinUI 3 con Windows App SDK sobre .NET 10. Las librerías de validación, una de ellas en VB.NET, pasaron a proyectos SDK-style con el mismo framework. En octubre se añadió el proyecto de empaquetado MSIX para volver a publicar en la tienda con la identidad original.

## Funcionalidades

| Módulo | Qué hace |
|---|---|
| Comprueba NIF | Valida el formato y la letra de control de un NIF o un NIE |
| Comprueba CCC | Valida los cuatro campos del Código Cuenta Cliente (banco, oficina, dígitos de control y cuenta) |
| Comprueba IBAN | Valida un IBAN según la norma ISO 13616 |

Todo el cálculo se hace en local. La app no usa la red ni envía datos a ningún sitio.

## Estructura de la solución

```
SuperComprobador_PRO.sln
├── SuperComrprobador_UWP          App WinUI 3 (.NET 10)
│   ├── Views/                     Páginas XAML: Main, P1NIF, P2CCC, P3Iban, Settings
│   ├── ViewModels/                Lógica de presentación (Observable + RelayCommand)
│   ├── Services/                  Activación, navegación y selector de tema
│   ├── Models/                    Modelos del menú de inicio
│   ├── Helpers/                   Converters y utilidades
│   └── Styles/                    Diccionarios de recursos XAML
├── SuperComrprobador_UWP.Core     Helpers compartidos (.NET 10)
├── CheckCCC                       Validación de CCC (VB.NET, .NET 10)
├── IbanLibraryPCL                 Validación de IBAN (C#, .NET 10)
└── SuperComrprobador_UWP_Package  Proyecto de empaquetado MSIX para la tienda
```

La carpeta `SuperComprobador` contiene la versión de Windows 8.1 y no se compila.

## Requisitos

- Windows 10 versión 1903 (compilación 18362) o superior.
- Visual Studio 2022 o 2026 con la carga de trabajo de desarrollo de Windows App SDK.
- SDK de Windows 10.0.26100.

## Compilar y ejecutar

1. Clona el repositorio.
   ```bash
   git clone https://github.com/ebenito/UWP_Supercomprobador.git
   ```
2. Abre `SuperComprobador_PRO.sln` en Visual Studio.
3. Elige la plataforma x64 o ARM64. La app no admite x86 ni AnyCPU.
4. Marca `SuperComrprobador_UWP` como proyecto de inicio y pulsa F5.

Desde la línea de comandos también compila con el MSBuild de Visual Studio:

```powershell
msbuild SuperComprobador_PRO.sln -restore -p:Configuration=Release -p:Platform=x64
```

## Empaquetar para Microsoft Store

El proyecto `SuperComrprobador_UWP_Package` genera el MSIX que se sube a Partner Center. Mantiene la identidad y el editor de la app original, de modo que la publicación llega como actualización a quien ya la tenía instalada.

1. Asocia el proyecto con la app en la tienda desde Visual Studio (botón derecho sobre el proyecto de empaquetado, Publicar, Asociar aplicación con la tienda). Esto genera `Package.StoreAssociation.xml`, que está excluido de git.
2. Con el mismo menú, crea los paquetes de aplicación para x64 y ARM64 en modo tienda. El resultado son ficheros `.msixupload` en `SuperComrprobador_UWP_Package/AppPackages`.
3. Sube esos ficheros en un nuevo envío de Partner Center.

Dos detalles que conviene tener presentes:

- La versión del manifiesto del proyecto de empaquetado debe ser mayor que la última publicada. La versión UWP anterior se publicó como 1.3.1.7, por eso la actual arranca en 2.0.0.0.
- Para instalar el paquete en local hace falta un certificado de pruebas. El fichero `.pfx` está excluido de git, así que en un clon nuevo hay que regenerarlo desde el diseñador del manifiesto, en la pestaña Empaquetado. Para la tienda no es necesario, porque firma Microsoft.

## Notas de la migración a WinUI 3

Los cambios que más trabajo dieron al pasar de UWP a WinUI 3, por si sirven a alguien que esté en las mismas.

- **Target framework.** De `uap10.0.19041` a `net10.0-windows10.0.26100.0` con `UseWinUI` activado.
- **Espacios de nombres.** Todo `Windows.UI.Xaml` pasa a `Microsoft.UI.Xaml`, tanto en C# como en XAML.
- **Ventana explícita.** WinUI 3 no tiene ventana implícita. En `App.xaml.cs` se crea un `Window`, se activa y se pasa al servicio de activación. `Window.Current` deja de existir, así que el servicio de navegación obtiene el `Frame` desde la ventana principal.
- **Recursos de controles.** `XamlControlsResources` tiene que ser el primer diccionario combinado en `App.xaml`. Sin él, los pinceles de tema no se resuelven y la app lanza una `XamlParseException` al arrancar.
- **Plataformas.** Solo x64 y ARM64. Cada proyecto referenciado por el empaquetado declara `RuntimeIdentifiers` con `win-x64` y `win-arm64`, porque de lo contrario el wapproj falla con el error NETSDK1047.
- **Servicios de tienda.** `StoreServicesFeedbackLauncher` no funciona en apps de escritorio con WinUI 3, por lo que el enlace al Centro de opiniones está oculto.

## Referencias

- [Migrar de UWP a Windows App SDK](https://learn.microsoft.com/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Windows App SDK](https://learn.microsoft.com/windows/apps/windows-app-sdk/)
- [Empaquetar una app de escritorio con un proyecto de empaquetado](https://learn.microsoft.com/windows/msix/desktop/desktop-to-uwp-packaging-dot-net)
