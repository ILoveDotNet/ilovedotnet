var builder = DistributedApplication.CreateBuilder(args);

#pragma warning disable ASPIREBROWSERLOGS001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var ilovedotnetWeb = builder.AddProject<Projects.Web>("ILoveDotNet-Web")
    .WithUrlForEndpoint("https", u => u.DisplayText = "Landing Page")
    .WithBrowserLogs();
#pragma warning restore ASPIREBROWSERLOGS001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

// builder.AddMauiProject<Projects.MAUI>("ILoveDotNet-MAUI", "../MAUI/MAUI.csproj");

builder.Build().Run();
