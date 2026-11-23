using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

if (builder.Configuration.GetValue<bool>("Testing:UseStandaloneWeb"))
{
  builder.AddProject<Projects.Web>("ILoveDotNet-Web");
}
else
{
#pragma warning disable ASPIREBLAZOR001
  var ilovedotnetWeb = builder.AddBlazorWasmProject<Projects.Web>("ilovedotnet");

#pragma warning disable ASPIREBROWSERLOGS001 // Browser log capture is experimental in this Aspire version.
  builder.AddBlazorGateway("gateway")
      .WithExternalHttpEndpoints()
      .WithOtlpExporter(OtlpProtocol.HttpProtobuf)
      .WithBlazorClientApp(ilovedotnetWeb)
      .WithBrowserLogs();
#pragma warning restore ASPIREBROWSERLOGS001
#pragma warning restore ASPIREBLAZOR001
}

// builder.AddMauiProject<Projects.MAUI>("ILoveDotNet-MAUI", "../MAUI/MAUI.csproj");

builder.Build().Run();
