using MCPServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<KnowledgeBaseService>();
builder.Services
    .AddMcpServer()
    .WithHttpTransport(options => options.Stateless = true)
    .WithToolsFromAssembly();

var app = builder.Build();

app.MapMcp("/mcp");

app.Run();

