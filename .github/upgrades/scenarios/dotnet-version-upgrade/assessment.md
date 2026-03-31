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

  - [SmartJobTracker.API\SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj)
  - [SmartJobTracker.Tests\SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 2 | 0 require upgrade |
| Total NuGet Packages | 14 | All compatible |
| Total Code Files | 24 |  |
| Total Code Files with Incidents | 0 |  |
| Total Lines of Code | 1336 |  |
| Total Number of Issues | 0 |  |
| Estimated LOC to modify | 0+ | at least 0.0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [SmartJobTracker.API\SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | net10.0 | ✅ None | 0 | 0 |  | AspNetCore, Sdk Style = True |
| [SmartJobTracker.Tests\SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj) | net10.0 | ✅ None | 0 | 0 |  | DotNetCoreApp, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 14 | 100.0% |
| ⚠️ Incompatible | 0 | 0.0% |
| 🔄 Upgrade Recommended | 0 | 0.0% |
| ***Total NuGet Packages*** | ***14*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| coverlet.collector | 6.0.2 |  | [SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj) | ✅Compatible |
| Dapper | 2.1.72 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| Microsoft.AspNetCore.Mvc.Testing | 9.0.14 |  | [SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj) | ✅Compatible |
| Microsoft.AspNetCore.OpenApi | 9.0.14 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.14 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| Microsoft.EntityFrameworkCore.Tools | 9.0.14 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| Microsoft.Extensions.Http | 9.0.14 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| Microsoft.NET.Test.Sdk | 17.12.0 |  | [SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj) | ✅Compatible |
| Microsoft.SemanticKernel | 1.74.0 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| Microsoft.SemanticKernel.Connectors.AzureOpenAI | 1.74.0 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| Moq | 4.20.72 |  | [SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj) | ✅Compatible |
| Swashbuckle.AspNetCore | 9.0.6 |  | [SmartJobTracker.API.csproj](#smartjobtrackerapismartjobtrackerapicsproj) | ✅Compatible |
| xunit | 2.9.2 |  | [SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj) | ✅Compatible |
| xunit.runner.visualstudio | 2.8.2 |  | [SmartJobTracker.Tests.csproj](#smartjobtrackertestssmartjobtrackertestscsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;SmartJobTracker.API.csproj</b><br/><small>net10.0</small>"]
    P2["<b>📦&nbsp;SmartJobTracker.Tests.csproj</b><br/><small>net10.0</small>"]
    P2 --> P1
    click P1 "#smartjobtrackerapismartjobtrackerapicsproj"
    click P2 "#smartjobtrackertestssmartjobtrackertestscsproj"

```

## Project Details

<a id="smartjobtrackerapismartjobtrackerapicsproj"></a>
### SmartJobTracker.API\SmartJobTracker.API.csproj

#### Project Info

- **Current Target Framework:** net10.0✅
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 25
- **Lines of Code**: 1158
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P2["<b>📦&nbsp;SmartJobTracker.Tests.csproj</b><br/><small>net10.0</small>"]
        click P2 "#smartjobtrackertestssmartjobtrackertestscsproj"
    end
    subgraph current["SmartJobTracker.API.csproj"]
        MAIN["<b>📦&nbsp;SmartJobTracker.API.csproj</b><br/><small>net10.0</small>"]
        click MAIN "#smartjobtrackerapismartjobtrackerapicsproj"
    end
    P2 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

<a id="smartjobtrackertestssmartjobtrackertestscsproj"></a>
### SmartJobTracker.Tests\SmartJobTracker.Tests.csproj

#### Project Info

- **Current Target Framework:** net10.0✅
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 3
- **Lines of Code**: 178
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["SmartJobTracker.Tests.csproj"]
        MAIN["<b>📦&nbsp;SmartJobTracker.Tests.csproj</b><br/><small>net10.0</small>"]
        click MAIN "#smartjobtrackertestssmartjobtrackertestscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;SmartJobTracker.API.csproj</b><br/><small>net10.0</small>"]
        click P1 "#smartjobtrackerapismartjobtrackerapicsproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

