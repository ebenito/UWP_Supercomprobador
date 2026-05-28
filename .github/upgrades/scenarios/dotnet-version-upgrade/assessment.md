# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [CheckCCC\CheckCCC.vbproj](#checkccccheckcccvbproj)
  - [IbanLibraryPCL\IbanLibraryPCL.csproj](#ibanlibrarypclibanlibrarypclcsproj)
  - [SuperComrprobador_UWP.Core\SuperComrprobador_UWP.Core.csproj](#supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj)
  - [SuperComrprobador_UWP\SuperComrprobador_UWP.csproj](#supercomrprobador_uwpsupercomrprobador_uwpcsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 4 | All require upgrade |
| Total NuGet Packages | 5 | 2 need upgrade |
| Total Code Files | 33 |  |
| Total Code Files with Incidents | 10 |  |
| Total Lines of Code | 1615 |  |
| Total Number of Issues | 21 |  |
| Estimated LOC to modify | 11+ | at least 0,7% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [CheckCCC\CheckCCC.vbproj](#checkccccheckcccvbproj) | .NETPortable,Version=v4.5 | 🟢 Low | 0 | 0 |  | ClassicClassLibrary, Sdk Style = False |
| [IbanLibraryPCL\IbanLibraryPCL.csproj](#ibanlibrarypclibanlibrarypclcsproj) | .NETPortable,Version=v4.5 | 🟢 Low | 0 | 0 |  | ClassicClassLibrary, Sdk Style = False |
| [SuperComrprobador_UWP.Core\SuperComrprobador_UWP.Core.csproj](#supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj) | netstandard2.0 | 🟢 Low | 2 | 0 |  | ClassLibrary, Sdk Style = True |
| [SuperComrprobador_UWP\SuperComrprobador_UWP.csproj](#supercomrprobador_uwpsupercomrprobador_uwpcsproj) | net5.0 | 🟢 Low | 2 | 11 | 11+ | Uwp, Sdk Style = False |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 3 | 60,0% |
| ⚠️ Incompatible | 1 | 20,0% |
| 🔄 Upgrade Recommended | 1 | 20,0% |
| ***Total NuGet Packages*** | ***5*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 1 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 10 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1461 |  |
| ***Total APIs Analyzed*** | ***1472*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| Microsoft.NETCore.UniversalWindowsPlatform | 6.2.12 |  | [SuperComrprobador_UWP.csproj](#supercomrprobador_uwpsupercomrprobador_uwpcsproj) | Needs to be replaced with Replace with new package Microsoft.WindowsAppSDK=2.1.3;Microsoft.Graphics.Win2D=1.1.0;Microsoft.Windows.Compatibility=10.0.8 |
| Microsoft.Services.Store.Engagement | 10.1901.28001 |  | [SuperComrprobador_UWP.csproj](#supercomrprobador_uwpsupercomrprobador_uwpcsproj) | ✅Compatible |
| Microsoft.Xaml.Behaviors.Uwp.Managed | 2.0.1 |  | [SuperComrprobador_UWP.csproj](#supercomrprobador_uwpsupercomrprobador_uwpcsproj) | ⚠️El paquete NuGet no es compatible |
| NETStandard.Library | 2.0.3 |  | [SuperComrprobador_UWP.Core.csproj](#supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj) | ✅Compatible |
| Newtonsoft.Json | 12.0.3 | 13.0.4 | [SuperComrprobador_UWP.Core.csproj](#supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj) | Se recomienda actualizar el paquete NuGet |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| T:System.Uri | 5 | 45,5% | Behavioral Change |
| M:System.Uri.#ctor(System.String) | 5 | 45,5% | Behavioral Change |
| T:System.WindowsRuntimeSystemExtensions | 1 | 9,1% | Source Incompatible |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>⚙️&nbsp;CheckCCC.vbproj</b><br/><small>.NETPortable,Version=v4.5</small>"]
    P2["<b>⚙️&nbsp;IbanLibraryPCL.csproj</b><br/><small>.NETPortable,Version=v4.5</small>"]
    P3["<b>⚙️&nbsp;SuperComrprobador_UWP.csproj</b><br/><small>net5.0</small>"]
    P4["<b>📦&nbsp;SuperComrprobador_UWP.Core.csproj</b><br/><small>netstandard2.0</small>"]
    P3 --> P1
    P3 --> P2
    P3 --> P4
    click P1 "#checkccccheckcccvbproj"
    click P2 "#ibanlibrarypclibanlibrarypclcsproj"
    click P3 "#supercomrprobador_uwpsupercomrprobador_uwpcsproj"
    click P4 "#supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj"

```

## Project Details

<a id="checkccccheckcccvbproj"></a>
### CheckCCC\CheckCCC.vbproj

#### Project Info

- **Current Target Framework:** .NETPortable,Version=v4.5
- **Proposed Target Framework:** net10.0
- **SDK-style**: False
- **Project Kind:** ClassicClassLibrary
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 3
- **Number of Files with Incidents**: 1
- **Lines of Code**: 129
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P3["<b>⚙️&nbsp;SuperComrprobador_UWP.csproj</b><br/><small>net5.0</small>"]
        click P3 "#supercomrprobador_uwpsupercomrprobador_uwpcsproj"
    end
    subgraph current["CheckCCC.vbproj"]
        MAIN["<b>⚙️&nbsp;CheckCCC.vbproj</b><br/><small>.NETPortable,Version=v4.5</small>"]
        click MAIN "#checkccccheckcccvbproj"
    end
    P3 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 26 |  |
| ***Total APIs Analyzed*** | ***26*** |  |

<a id="ibanlibrarypclibanlibrarypclcsproj"></a>
### IbanLibraryPCL\IbanLibraryPCL.csproj

#### Project Info

- **Current Target Framework:** .NETPortable,Version=v4.5
- **Proposed Target Framework:** net10.0
- **SDK-style**: False
- **Project Kind:** ClassicClassLibrary
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 3
- **Number of Files with Incidents**: 1
- **Lines of Code**: 325
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P3["<b>⚙️&nbsp;SuperComrprobador_UWP.csproj</b><br/><small>net5.0</small>"]
        click P3 "#supercomrprobador_uwpsupercomrprobador_uwpcsproj"
    end
    subgraph current["IbanLibraryPCL.csproj"]
        MAIN["<b>⚙️&nbsp;IbanLibraryPCL.csproj</b><br/><small>.NETPortable,Version=v4.5</small>"]
        click MAIN "#ibanlibrarypclibanlibrarypclcsproj"
    end
    P3 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 281 |  |
| ***Total APIs Analyzed*** | ***281*** |  |

<a id="supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj"></a>
### SuperComrprobador_UWP.Core\SuperComrprobador_UWP.Core.csproj

#### Project Info

- **Current Target Framework:** netstandard2.0✅
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 2
- **Number of Files with Incidents**: 1
- **Lines of Code**: 45
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P3["<b>⚙️&nbsp;SuperComrprobador_UWP.csproj</b><br/><small>net5.0</small>"]
        click P3 "#supercomrprobador_uwpsupercomrprobador_uwpcsproj"
    end
    subgraph current["SuperComrprobador_UWP.Core.csproj"]
        MAIN["<b>📦&nbsp;SuperComrprobador_UWP.Core.csproj</b><br/><small>netstandard2.0</small>"]
        click MAIN "#supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj"
    end
    P3 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 18 |  |
| ***Total APIs Analyzed*** | ***18*** |  |

<a id="supercomrprobador_uwpsupercomrprobador_uwpcsproj"></a>
### SuperComrprobador_UWP\SuperComrprobador_UWP.csproj

#### Project Info

- **Current Target Framework:** net5.0
- **Proposed Target Framework:** net10.0-windows10.0.26100.0
- **SDK-style**: False
- **Project Kind:** Uwp
- **Dependencies**: 3
- **Dependants**: 0
- **Number of Files**: 40
- **Number of Files with Incidents**: 7
- **Lines of Code**: 1116
- **Estimated LOC to modify**: 11+ (at least 1,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["SuperComrprobador_UWP.csproj"]
        MAIN["<b>⚙️&nbsp;SuperComrprobador_UWP.csproj</b><br/><small>net5.0</small>"]
        click MAIN "#supercomrprobador_uwpsupercomrprobador_uwpcsproj"
    end
    subgraph downstream["Dependencies (3"]
        P1["<b>⚙️&nbsp;CheckCCC.vbproj</b><br/><small>.NETPortable,Version=v4.5</small>"]
        P2["<b>⚙️&nbsp;IbanLibraryPCL.csproj</b><br/><small>.NETPortable,Version=v4.5</small>"]
        P4["<b>📦&nbsp;SuperComrprobador_UWP.Core.csproj</b><br/><small>netstandard2.0</small>"]
        click P1 "#checkccccheckcccvbproj"
        click P2 "#ibanlibrarypclibanlibrarypclcsproj"
        click P4 "#supercomrprobador_uwpcoresupercomrprobador_uwpcorecsproj"
    end
    MAIN --> P1
    MAIN --> P2
    MAIN --> P4

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 1 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 10 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1136 |  |
| ***Total APIs Analyzed*** | ***1147*** |  |

