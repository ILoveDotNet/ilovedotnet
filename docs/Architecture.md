# ILoveDotNet Application Architecture

This document provides a visual representation of the ILoveDotNet application architecture using Mermaid diagrams.

## Project Structure

The following diagram illustrates the high-level project structure and relationships between the main components:

```mermaid
graph TB
    subgraph "Entry Points"
        Web["Web Project<br>(Blazor WASM)"]
        MAUI["MAUI Project<br>(Cross-platform native app)"]
    end

    subgraph "Core Components"
        BaseComponents["BaseComponents<br>(Core UI elements)"]
        CommonComponents["CommonComponents<br>(Shared UI elements)"]
        SharedComponents["SharedComponents<br>(Utility components)"]
        SharedModels["SharedModels<br>(Data models)"]
    end

    subgraph "Demo Components" 
        BlazorDemo["BlazorDemoComponents"]
        LINQDemo["LINQDemoComponents"]
        DIDemoComponents["DependencyInjectionDemoComponents"]
        DesignPatternDemo["DesignPatternDemoComponents"]
        OOPSDemo["OOPSDemoComponents"]
        RegexDemo["RegexDemoComponents"]
        ReportDemo["ReportDemoComponents"]
        HTTPDemo["HTTPClientDemoComponents"]
        MiddlewareDemo["MiddlewareDemoComponents"]
        MLNETDemo["MLNETDemoComponents"]
        WebAPIDemo["WebAPIDemoComponents"]
        MAUIDemo["MAUIDemoComponents"]
        MSBuildDemo["MSBuildDemoComponents"]
        OWASPDemo["OWASPDemoComponents"]
        SignalRDemo["SignalRDemoComponents"]
        SOLIDDemo["SOLIDDemoComponents"]
        TDDDemo["TDDDemoComponents"]
        TestingDemo["TestingDemoComponents"]
        PythonDemo["PythonDemoComponents"]
        TalkDemo["TalkDemoComponents"]
    end

    subgraph "Utilities"
        RSSFeedGenerator["RSS Feed Generator"]
        SitemapGenerator["Sitemap Generator"]
        UITests["UI Tests"]
    end

    %% Connections between components
    Web --> BaseComponents
    Web --> CommonComponents
    Web --> SharedComponents
    Web --> SharedModels
    Web --> BlazorDemo
    Web --> LINQDemo
    Web --> DIDemoComponents
    Web --> DesignPatternDemo
    Web --> OOPSDemo
    Web --> RegexDemo
    Web --> ReportDemo
    Web --> HTTPDemo
    Web --> MiddlewareDemo
    Web --> MLNETDemo
    Web --> WebAPIDemo
    Web --> MAUIDemo
    Web --> MSBuildDemo
    Web --> OWASPDemo
    Web --> SignalRDemo
    Web --> SOLIDDemo
    Web --> TDDDemo
    Web --> TestingDemo
    Web --> PythonDemo
    Web --> TalkDemo

    MAUI --> BaseComponents
    MAUI --> CommonComponents
    MAUI --> SharedComponents
    MAUI --> SharedModels
    MAUI --> BlazorDemo
    MAUI --> LINQDemo
    MAUI --> DIDemoComponents
    MAUI --> DesignPatternDemo
    MAUI --> OOPSDemo
    MAUI --> RegexDemo
    MAUI --> ReportDemo
    MAUI --> HTTPDemo
    MAUI --> MiddlewareDemo
    MAUI --> MLNETDemo
    MAUI --> WebAPIDemo
    MAUI --> MAUIDemo
    MAUI --> MSBuildDemo
    MAUI --> OWASPDemo
    MAUI --> SignalRDemo
    MAUI --> SOLIDDemo
    MAUI --> TDDDemo
    MAUI --> TestingDemo
    MAUI --> PythonDemo
    MAUI --> TalkDemo

    %% Layout control
    classDef entryPoint fill:#f96,stroke:#333,stroke-width:2px
    classDef coreComponent fill:#bbf,stroke:#333,stroke-width:1px
    classDef demoComponent fill:#bfb,stroke:#333,stroke-width:1px
    classDef utility fill:#fbb,stroke:#333,stroke-width:1px

    class Web,MAUI entryPoint
    class BaseComponents,CommonComponents,SharedComponents,SharedModels coreComponent
    class BlazorDemo,LINQDemo,DIDemoComponents,DesignPatternDemo,OOPSDemo,RegexDemo,ReportDemo,HTTPDemo,MiddlewareDemo,MLNETDemo,WebAPIDemo,MAUIDemo,MSBuildDemo,OWASPDemo,SignalRDemo,SOLIDDemo,TDDDemo,TestingDemo,PythonDemo,TalkDemo demoComponent
    class RSSFeedGenerator,SitemapGenerator,UITests utility
```

## MAUI WebView Architecture

The following diagram illustrates how MAUI.csproj uses BlazorWebView to render the same web content as the Web.csproj project but inside a native shell:

```mermaid
flowchart TB
    subgraph "MAUI Native Application"
        MauiShell["MAUI Shell<br>(Native Container)"]
        BlazorWebView["BlazorWebView<br>(WebView Control)"]
        
        subgraph "Web Content (Same as Web.csproj)"
            Router["Router<br>(Routes.razor)"]
            MainLayout["MainLayout"]
            LazyLoading["Lazy Loading"]
            DemoComponents["Demo Components"]
        end
        
        MauiShell -->|"Hosts"| BlazorWebView
        BlazorWebView -->|"Renders"| Router
        Router --> MainLayout
        MainLayout --> LazyLoading
        LazyLoading --> DemoComponents
    end
    
    subgraph "Platform Native Features"
        Notifications["Native Notifications"]
        FileSystem["File System Access"]
        DeviceFeatures["Device Features"]
    end
    
    MauiShell -.->|"Provides access to"| Notifications
    MauiShell -.->|"Provides access to"| FileSystem
    MauiShell -.->|"Provides access to"| DeviceFeatures
    
    classDef native fill:#f96,stroke:#333,stroke-width:2px
    classDef webview fill:#bbf,stroke:#333,stroke-width:2px
    classDef webcontent fill:#bfb,stroke:#333,stroke-width:1px
    classDef platformfeatures fill:#fbb,stroke:#333,stroke-width:1px
    
    class MauiShell native
    class BlazorWebView webview
    class Router,MainLayout,LazyLoading,DemoComponents webcontent
    class Notifications,FileSystem,DeviceFeatures platformfeatures
```

## Component Interaction and Lazy Loading

The following sequence diagram illustrates how components interact and the lazy loading mechanism:

```mermaid
sequenceDiagram
    participant User
    participant Browser
    participant Web as Web/MAUI Entry Point
    participant LazyLoader as Lazy Loader Service
    participant Components as Component Libraries
    participant JSInterop as JavaScript Interop
    
    User->>Browser: Open application
    Browser->>Web: Load initial resources
    Web->>LazyLoader: Initialize
    LazyLoader->>Web: Pre-load critical assemblies
    Web->>Browser: Render initial UI
    
    Note over User,Browser: User navigates to a topic
    
    User->>Browser: Click navigation link
    Browser->>Web: Navigate to route
    Web->>LazyLoader: OnNavigateAsync()
    LazyLoader->>Components: Load required assembly
    Components->>Web: Assembly loaded
    Web->>Browser: Render component
    
    Note over User,Browser: User interacts with demo
    
    User->>Browser: Interact with demo
    Browser->>Web: Component event
    Web->>JSInterop: Call JavaScript when needed
    JSInterop->>Browser: Execute client-side code
    Browser->>User: Display result
```

## Deployment Architecture

The following diagram shows the deployment process and targets:

```mermaid
flowchart TD
    subgraph "Development"
        Dev["Developer Workstation"]
        GitHub["GitHub Repository"]
    end
    
    subgraph "CI/CD"
        Actions["GitHub Actions"]
        BuildWeb["Build Web App"]
        BuildMAUI["Build MAUI Apps"]
        Tests["Run Tests"]
    end
    
    subgraph "Deployment Targets"
        GHPages["GitHub Pages<br>(Web)"]
        AppStores["App Stores<br>(iOS/Android/Windows)"]
        Docker["Docker Container"]
    end
    
    Dev -->|Push Changes| GitHub
    GitHub -->|Trigger Workflow| Actions
    Actions --> BuildWeb
    Actions --> BuildMAUI
    Actions --> Tests
    BuildWeb -->|Deploy| GHPages
    BuildMAUI -->|Publish| AppStores
    BuildWeb -->|Package| Docker
    
    classDef dev fill:#f9f,stroke:#333,stroke-width:1px
    classDef cicd fill:#ff9,stroke:#333,stroke-width:1px
    classDef deploy fill:#9ff,stroke:#333,stroke-width:1px
    
    class Dev,GitHub dev
    class Actions,BuildWeb,BuildMAUI,Tests cicd
    class GHPages,AppStores,Docker deploy
```

## Key Architectural Characteristics

1. **Modular Design**: The application is structured into multiple component libraries, each focusing on a specific aspect of .NET technology.

2. **Dual Entry Points**: 
   - **Web Project (Blazor WASM)**: For browser-based access
   - **MAUI Project**: For native application access across multiple platforms (iOS, Android, macOS, Windows)
   - **MAUI as a Shell**: The MAUI project uses BlazorWebView to render the same Blazor WASM components as the Web project but inside a native application shell, providing access to native platform features while maintaining a unified codebase

3. **Lazy Loading**: Components are loaded on-demand to optimize initial load time and performance.

4. **Shared Component Model**: Both entry points share the same component libraries, maximizing code reuse.

5. **JavaScript Interoperability**: Uses JSInterop for browser-native functionality when needed.

6. **Responsive Design**: Adapts to different screen sizes and platforms.

7. **CI/CD Integration**: Automated build and deployment through GitHub Actions.

8. **Testing**: Includes UI testing capabilities for ensuring quality.

## Component Types

1. **Core Components**: Basic building blocks used across the application
   - BaseComponents
   - CommonComponents
   - SharedComponents
   - SharedModels

2. **Demo Components**: Topic-specific components that demonstrate various .NET technologies
   - BlazorDemoComponents
   - LINQDemoComponents
   - DependencyInjectionDemoComponents
   - Many others (20+ topic areas)

3. **Utilities**: Supporting tools and services
   - RSSFeedGenerator
   - SitemapGenerator
   - UITests

## Technical Implementation

- **Language**: C# with Razor components
- **Framework**: .NET 9.0 with Blazor WebAssembly and .NET MAUI
- **Styling**: CSS with custom classes
- **JavaScript Integration**: Module-based JS isolation
- **Testing**: XUnit with coverage reporting
- **MAUI Implementation**: Uses BlazorWebView to host the same web components as the Web project inside a native shell, providing a unified experience with native platform capabilities
